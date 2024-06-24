using System;
using System.Threading.Tasks;
using dotenv.net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ecommerceApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Load environment variables from .env file
            DotEnv.Load();

            var clientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
            var tenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID");
            var clientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET");

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            var searchServiceName = Environment.GetEnvironmentVariable("SEARCH_SERVICE_NAME");
            var apiKey = Environment.GetEnvironmentVariable("SEARCH_SERVICE_API_KEY");
            var searchIndexName = Environment.GetEnvironmentVariable("SEARCH_INDEX_NAME");

            var dataFactoryName = Environment.GetEnvironmentVariable("DATA_FACTORY_NAME");            
            var pipelineName = Environment.GetEnvironmentVariable("PIPELINE_NAME");
            var resourceGroupName = Environment.GetEnvironmentVariable("RESOURCE_GROUP_NAME");
            var blobStorageConnectionString = Environment.GetEnvironmentVariable("BLOB_STORAGE_CONNECTION_STRING");
            var containerName = Environment.GetEnvironmentVariable("CONTAINER_NAME");
            var subscriptionId = Environment.GetEnvironmentVariable("SUBSCRIPTION_ID");    


            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(searchServiceName) || string.IsNullOrEmpty(apiKey) ||
                string.IsNullOrEmpty(dataFactoryName) || string.IsNullOrEmpty(resourceGroupName) || string.IsNullOrEmpty(blobStorageConnectionString) ||
                string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(subscriptionId)|| string.IsNullOrEmpty(searchIndexName)||
                string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(clientSecret) || string.IsNullOrEmpty(pipelineName))
            {
                Console.WriteLine("One or more environment variables are missing. Please check your .env file.");
                return;
            }

            var etlPipeline = new ETLPipeline(subscriptionId, clientId, tenantId, clientSecret, dataFactoryName, resourceGroupName, blobStorageConnectionString, containerName,pipelineName);
            var cognitiveSearchService = new CognitiveSearchService(searchServiceName, apiKey, searchIndexName);

            while (true)

            while (true)
            {
                Console.WriteLine("Enter a command:");
                Console.WriteLine("1. Search products");
                Console.WriteLine("2. Run ETL pipeline");
                Console.WriteLine("3. Capture changes");
                Console.WriteLine("4. Exit");

                var command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        Console.WriteLine("Enter a search query:");
                        var query = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(query))
                        {
                            Console.WriteLine("Please enter a valid search query.");
                        }
                        else
                        {
                            try
                            {
                                var searchResults = await cognitiveSearchService.SearchProductsAsync(query);
                                var orders = JsonConvert.SerializeObject(searchResults, Formatting.Indented);
                                Console.WriteLine(orders);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error searching products: {ex.Message}");
                            }
                        }
                        break;
                    case "2":
                        Console.WriteLine("Enter the local file path of the Excel file:");
                        var filePath = Console.ReadLine();
                        if (string.IsNullOrEmpty(filePath))
                        {
                            Console.WriteLine("Invalid file path. Please try again.");
                            break;
                        }
                        Console.WriteLine("Running ETL pipeline...");
                        try
                        {
                            await etlPipeline.RunPipelineAsync(filePath);
                            Console.WriteLine("ETL pipeline completed.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error running ETL pipeline: {ex.Message}");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Capturing changes...");
                        // Call the CaptureChanges method here
                        Console.WriteLine("Changes captured.");
                        break;
                    case "4":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            }
        }
    }
}
