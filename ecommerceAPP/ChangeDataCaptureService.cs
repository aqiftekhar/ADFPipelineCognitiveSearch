using System;
using System.Data.SqlClient;
using Azure.Storage.Blobs;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;

namespace ecommerceApp
{
    public class ChangeDataCaptureService
    {
        private readonly string _dbConnectionString;
        private readonly string _blobStorageConnectionString;
        private readonly string _containerName;

        public ChangeDataCaptureService(string dbConnectionString, string blobStorageConnectionString, string containerName)
        {
            _dbConnectionString = dbConnectionString;
            _blobStorageConnectionString = blobStorageConnectionString;
            _containerName = containerName;
        }

        public async Task CaptureChangesAsync()
        {
            try
            {
                using (var sourceConnection = new SqlConnection(_dbConnectionString))
                {
                    await sourceConnection.OpenAsync();

                    byte[] from_lsn;
                    byte[] to_lsn;

                    using (var command = new SqlCommand("SELECT sys.fn_cdc_get_min_lsn('dbo_Products')", sourceConnection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            reader.Read();
                            from_lsn = (byte[])reader[0];
                        }
                    }

                    using (var command = new SqlCommand("SELECT sys.fn_cdc_get_max_lsn()", sourceConnection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            reader.Read();
                            to_lsn = (byte[])reader[0];
                        }
                    }

                    string rowFilterOption = "all";

                    using (var command = new SqlCommand("SELECT * FROM cdc.fn_cdc_get_all_changes_dbo_Products(@from_lsn, @to_lsn, @row_filter_option)", sourceConnection))
                    {
                        command.Parameters.AddWithValue("@from_lsn", from_lsn);
                        command.Parameters.AddWithValue("@to_lsn", to_lsn);
                        command.Parameters.AddWithValue("@row_filter_option", rowFilterOption);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var changes = new List<dynamic>();

                            while (await reader.ReadAsync())
                            {
                                var change = new
                                {
                                    __start_lsn = reader["__$start_lsn"],
                                    __operation = reader["__$operation"],
                                    __update_mask = reader["__$update_mask"],
                                    product_id = reader["product_id"],
                                    product_name = reader["product_name"],
                                    price = reader["price"],
                                    description = reader["description"],
                                    image_url = reader["image_url"],
                                    date_added = reader["date_added"]
                                };

                                changes.Add(change);
                            }

                            var serializedChanges = JsonConvert.SerializeObject(changes, Formatting.Indented);

                            var blobServiceClient = new BlobServiceClient(_blobStorageConnectionString);
                            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
                            var blobClient = containerClient.GetBlobClient("changes.json");

                            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(serializedChanges)))
                            {
                                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = "application/json" });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

    }
}
