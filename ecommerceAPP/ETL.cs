using Azure;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.Rest;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ecommerceApp
{
    public class ETLPipeline
    {
        private readonly string _dataFactoryName;
        private readonly string _resourceGroupName;
        private readonly string _blobStorageConnectionString;
        private readonly string _containerName;
        private readonly string _subscriptionId;

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _tenantId;

        private readonly string _pipelineName;

        public ETLPipeline(string subscriptionId,string clientId, string tenantId, string clientSecret, string dataFactoryName, string resourceGroupName, string blobStorageConnectionString, string containerName,  string pipelineName)
        {
            _subscriptionId = subscriptionId;
            _clientId = clientId;
            _tenantId= tenantId;
            _clientSecret = clientSecret;
            _dataFactoryName = dataFactoryName;
            _resourceGroupName = resourceGroupName;
            _blobStorageConnectionString = blobStorageConnectionString;
            _containerName = containerName;
            _pipelineName = pipelineName;
        }

        public async Task RunPipelineAsync(string filePath)
        {
            Console.WriteLine(filePath);
            // Upload the file to Blob storage
            await UploadFileToBlobStorageAsync(filePath);

            // Trigger the ETL pipeline in ADF
            await TriggerPipelineAsync();
        }

        private async Task UploadFileToBlobStorageAsync(string filePath)
        {
            var blobClient = new BlobClient(_blobStorageConnectionString, _containerName, Path.GetFileName(filePath));
            using (var fileStream = File.OpenRead(filePath))
            {
                await blobClient.UploadAsync(fileStream, overwrite: true);
            }
            Console.WriteLine("File uploaded successfully.");
        }

        private async Task TriggerPipelineAsync()
        {
            var credential = new ClientSecretCredential(_tenantId, _clientId, _clientSecret);

            var tokenRequestContext = new Azure.Core.TokenRequestContext(new[] { "https://management.azure.com/.default" });
            var accessToken = (await credential.GetTokenAsync(tokenRequestContext)).Token;

            var serviceClientCredentials = new TokenCredentials(accessToken);

            var dataFactoryClient = new DataFactoryManagementClient(serviceClientCredentials)
            {
                SubscriptionId = _subscriptionId
            };

            var pipelineRun = await dataFactoryClient.Pipelines.CreateRunWithHttpMessagesAsync(
                _resourceGroupName, _dataFactoryName, _pipelineName);

            if (pipelineRun.Response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine($"ETL pipeline executed successfully. The Status code is : {pipelineRun.Response.StatusCode}");
            } else 
            {
                Console.WriteLine($"Something Went Wrong. The Status code is : {pipelineRun.Response.StatusCode}");
            }
        }
    }
}
