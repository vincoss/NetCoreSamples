using Azure.Storage.Blobs;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.Sql.Fluent.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals_AzureKeyValut_TestSamples.Services
{
    public class AzureBlobService
    {
        public async Task UploadAsync(string sasUrl, string fileName, Stream content)
        {
            BlobContainerClient _container = new BlobContainerClient(new Uri(sasUrl));
            BlobClient blob = _container.GetBlobClient(fileName);

            var blobName = $"{Guid.NewGuid()}/{fileName}";
            await blob.UploadAsync(content);
        }
    }
}
