using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fundamentals_AzureKeyValut_TestSamples
{
    public class AzureService
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/azure/storage/blobs/sas-service-create?tabs=dotnet
        /// </summary>
        public Uri GetSasUriRead(string connectionString, string fileName, string displayName, string containerName)
        {
            return GetSasUri(connectionString, fileName, displayName, containerName, BlobContainerSasPermissions.Read);
        }

        public Uri GetSasUriCreate(string connectionString, string fileName, string displayName, string containerName)
        {
            return GetSasUri(connectionString, fileName, displayName, containerName, BlobContainerSasPermissions.Create);
        }

        private Uri GetSasUri(string connectionString, string fileId, string displayName, string containerName, BlobContainerSasPermissions permissions)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrWhiteSpace(fileId)) throw new ArgumentNullException(nameof(fileId));
            if (string.IsNullOrWhiteSpace(containerName)) throw new ArgumentNullException(nameof(containerName));

            if (string.IsNullOrWhiteSpace(displayName))
            {
                displayName = fileId;
            }

            var client = new BlobServiceClient(connectionString);
            var container = client.GetBlobContainerClient(containerName.ToLower());
            var blob = container.GetBlobClient(fileId.ToLower());

            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = container.Name,
                Resource = "c",
                ContentDisposition = $"attachment; filename={displayName.ToLower()}"
            };

            sasBuilder.ExpiresOn = DateTime.UtcNow.AddMinutes(1);
            sasBuilder.SetPermissions(permissions);

            return blob.GenerateSasUri(sasBuilder);
        }
    }
}