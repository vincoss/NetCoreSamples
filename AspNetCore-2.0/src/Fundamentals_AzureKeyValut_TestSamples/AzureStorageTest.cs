using Microsoft.Azure;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using System.Reflection;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Threading;
using Fundamentals_AzureKeyValut_TestSamples.Services;

namespace Fundamentals_AzureKeyValut_TestSamples
{
    public class AzureStorageTest
    {
        [Fact]
        public void Test()
        {
            var storageName = "TODO-Storage name";
            var storageSecreet = "TODO-Storage secreet";

            var cred = new StorageCredentials(storageName, storageSecreet);
            CloudStorageAccount storageAccount = new CloudStorageAccount(cred, true);

            Assert.NotNull(storageAccount);
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/rest/api/datacatalog/authenticate-a-client-app
        /// </summary>
        [Fact]
        public async void T2()
        {
            var clientId = "TODO - clientID";
            var clientSecret = "TODO - secreet";
            var tenant = "TODO - tenant";
            var authority = $"https://login.windows.net/{tenant}";
            var resource = "https://vault.azure.net";
            var storageAccountName = "TODO - storage name";

            var authContext = new AuthenticationContext(authority);
            var credential = new ClientCredential(clientId, clientSecret);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, credential);
            if (result == null)
            {
                throw new InvalidOperationException("Failed to retrieve JWT token");
            }
            var token = result.AccessToken;

            StorageCredentials storageCredentials = new StorageCredentials(sasToken: token);
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials: storageCredentials, storageAccountName, null,  useHttps: true);

            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("TODO - container name");

            Assert.NotNull(container);
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/azure/authentication
        /// </summary>
        [Fact]
        public void T3()
        {
            var clientId = "TODO - client ID";
            var clientSecret = "TODO - secreet";
            var tenantId = "TODO - tenant ID";

            var credentials = SdkContext.AzureCredentialsFactory
                .FromServicePrincipal(clientId,
                    clientSecret,
                    tenantId,
                    AzureEnvironment.AzureGlobalCloud);

            var azure = Microsoft.Azure.Management.Fluent.Azure
        .Configure()
        .Authenticate(credentials)
        .WithDefaultSubscription();

            Assert.NotNull(azure);

            var storageName = "TODO - storage name";
            var f = azure.StorageAccounts.List().Where(x => x.Name == storageName).FirstOrDefault();
            var sk = f.GetKeys().First().Value;

            StorageCredentials storageCredentials = new StorageCredentials(storageName, sk);
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials: storageCredentials, storageName, null, useHttps: true);

            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("TODO - container name");

            Assert.NotNull(container);
        }

        [Fact]
        public async void PushFileToBlobWithSasTokenUrl()
        {
            var filePath = @"c:\temp\test.txt";
            var url = "SAS Token url here";

            var _client = new HttpClient();
            var content = new MultipartFormDataContent();
            var file = File.OpenRead(filePath);

            var fileContent = new StreamContent(file);

            content.Add(fileContent, "file", "test.txt");

            content.Headers.Add("x-ms-blob-type", "BlockBlob");

            var response = await _client.PutAsync(url, content);

            response.EnsureSuccessStatusCode();

             Assert.NotNull(response);
        }

        [Fact]
        public async void UploadLargeFile()
        {
            var filePath = @"C:\Temp\upload\8CC9EFF8-BFFC-4F8D-B434-FDF74A6C8793.exe";
            var fileName = Path.GetFileName(filePath);

            var url = "";
            await new AzureBlobService().UploadAsync(url, fileName, File.OpenRead(filePath));
        }

        [Fact]
        public async void ChunkedPushFileToBlobWithSasTokenUrl()
        {
            var filePath = @"C:\Temp\upload\Workstation-Installs-2022_3.7z";
            var fileName = Path.GetFileName(filePath);


            var url = "SAS URL";

             //          new AzureStorageApi().UploadFile(filePath, url);

          await new UploadManager(url).UploadStreamAsync(File.OpenRead(filePath), fileName);



            //var _client = new HttpClient();
            //var content = new MultipartFormDataContent();
            //var file = File.OpenRead(filePath);

            //var fileContent = new StreamContent(file);

            //content.Add(fileContent, "file", fileName);

            //content.Headers.Add("x-ms-blob-type", "BlockBlob");

            //var response = await _client.PutAsync(url, content);
            //response.EnsureSuccessStatusCode();

            //Assert.NotNull(response);

            //ulong totalBytesRead = 0UL;
            //int bufferSize = 1024 * 512; // bytes
            //byte[] buffer = new byte[bufferSize];
            //int bytesRead = 0;
            //int counter = 0;

            //using(var stream = File.OpenRead(filePath))
            //{
            //    while ((bytesRead = await stream.ReadAsync(buffer)) != 0)
            //    {
            //        counter++;
            //        totalBytesRead = totalBytesRead + (ulong)bytesRead;
            //        using (var tmpMemoryStream = new MemoryStream(buffer, 0, bytesRead))
            //        {
            //            await PutFileLarge(tmpMemoryStream, fileName, counter);

            //        }
            //    };
            //}


            //await CommitBlockList();
        }

        private static async Task PutFileLarge(Stream stream, string fileName, int currentBlockId)
        {
            var blobUrl = "SAS URL";

            var uriBuilder = new UriBuilder(blobUrl);
            //var currentBlockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
            uriBuilder.Query = uriBuilder.Query.TrimStart('?') + string.Format("&comp=block&blockid={0}", currentBlockId);
            var str = uriBuilder.Uri.ToString();

            if(str != null)
            {

            }

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = uriBuilder.Uri
            };

            using (var _client = new HttpClient())
            {
                var length = stream.Length.ToString();
                var streamContent = new StreamContent(stream);
                streamContent.Headers.Add("x-ms-blob-type", "BlockBlob");
                request.Content = streamContent;

                var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                response.EnsureSuccessStatusCode();
            }

            blockIds.Add(currentBlockId);
        }

        static List<int> blockIds = new List<int>();

        private static async Task CommitBlockList()
        {
            var blobUrl = "SAS URL";

            var uri = new Uri(string.Format("{0}&comp=blocklist", blobUrl));

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = uri
            };

            using (var _client = new HttpClient())
            {
                using (var ms = new MemoryStream())
                {
                    var streamContent = new StreamContent(ms);

                    var document = new XDocument(
                       new XElement("BlockList",
                           from blockId in blockIds
                           select new XElement("Uncommitted", blockId)));
                    var writer = XmlWriter.Create(ms, new XmlWriterSettings() { Encoding = Encoding.UTF8 });
                    document.Save(writer);
                    writer.Flush();

                    request.Content = streamContent;

                    var response = await _client.SendAsync(request);

                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public class AzureStorageApi
        {
            private const string AzureApiVersion = "2020-06-12";
            private const int BlockSize = 4 * 1024 * 1024;

            public void UploadFile(string fullFilePath, string blobSasUri, Dictionary<string, string> metadata = null, int timeout = 60)
            {
                var blocks = new List<String>();
                var tasks = new List<Task>();

                // Cancel Signal Source
                var cancelSignal = new CancellationTokenSource();
                using (var fileStream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read))
                {
                    var buffer = new byte[BlockSize];

                    var bytesRead = 0;
                    var blockNumber = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, BlockSize)) > 0)
                    {
                        var actualBytesRead = new byte[bytesRead];
                        // copy from old array to new one. 
                        // Need this to be a separate array because we are passing that to a task
                        Buffer.BlockCopy(buffer, 0, actualBytesRead, 0, bytesRead);
                        var blockId = blockNumber++.ToString().PadLeft(10, '0');
                        blocks.Add(blockId);

                        // here could've used Task.Factory.StartNew(), but this reads better
                        var task = new Task(() => UploadBlock(blobSasUri, blockId, actualBytesRead, timeout), cancelSignal.Token);
                        task.Start();
                        tasks.Add(task);
                    }
                }
                try
                {
                    // showing off here - chaining the tasks together. Don't need it here, but trying it out
                    var continuation = Task.Factory.ContinueWhenAll(tasks.ToArray(),
                        (t) => CommitAllBlocks(blobSasUri, blocks, metadata, timeout));
                    continuation.Wait();
                }
                catch (AggregateException exception)
                {
                    // when one of the tasks fail, we want to abort all other tasks, otherwise they will keep uploading.
                    // This is how we signal the cancellation to all other tasks
                    cancelSignal.Cancel();

                    // AggregateException.InnerException contains the actual exception that was thrown by a task.
                    throw exception.InnerException;
                }
            }



            private void UploadBlock(String sasUrl, String blockId, byte[] contentBytes, int timeout)
            {
                using (var client = GetHttpClient(timeout))
                {
                    var blockUrl = String.Format("{0}&comp=block&blockid={1}", sasUrl, blockId.EncodeToBase64String());

                    HttpContent content = new ByteArrayContent(contentBytes);
                    content.Headers.ContentLength = contentBytes.Length;
                    content.Headers.ContentMD5 = MD5.Create().ComputeHash(contentBytes);

                    var result = client.PutAsync(blockUrl, content);
                    ProcessResult(result);
                }
            }


            private void CommitAllBlocks(string sasUrl, List<string> blockIds, Dictionary<string, string> metadata, int timeout)
            {
                using (var client = GetHttpClient(timeout))
                {
                    var contentBuilder = new StringBuilder();
                    contentBuilder.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
                    contentBuilder.AppendLine("<BlockList>");
                    foreach (var blockId in blockIds)
                    {
                        contentBuilder.AppendFormat("<Uncommitted>{0}</Uncommitted>", blockId.EncodeToBase64String());
                    }
                    contentBuilder.AppendLine("</BlockList>");
                    var contentString = contentBuilder.ToString();

                    HttpContent content = new StringContent(contentString);
                    content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Text.Plain);
                    content.Headers.ContentLength = contentString.Length;

                    foreach (var pair in metadata ?? new Dictionary<string, string>())
                    {
                        content.Headers.Add("x-ms-meta-" + pair.Key, pair.Value);
                    }

                    var commitUrl = String.Format("{0}&comp=blocklist", sasUrl);

                    var result = client.PutAsync(commitUrl, content);
                    ProcessResult(result);
                }
            }


            private HttpClient GetHttpClient(int timeout)
            {
                var client = new HttpClient();
                client.Timeout = GetTimeout(timeout);
                client.DefaultRequestHeaders.Add("x-ms-date", DateTime.Now.ToUniversalTime().ToString("r"));
                client.DefaultRequestHeaders.Add("x-ms-version", AzureApiVersion);

                return client;
            }


            private TimeSpan GetTimeout(int specifiedTimeout)
            {
                if (specifiedTimeout == 0)
                {
                    return TimeSpan.FromMilliseconds(Timeout.Infinite);
                }
                return TimeSpan.FromMinutes(specifiedTimeout);
            }



            private HttpResponseMessage ProcessResult(Task<HttpResponseMessage> task)
            {
                HttpResponseMessage response = null;
                try
                {
                    response = task.Result;
                }
                catch (Exception exception)
                {
                    var innerExceptionMessage = exception.InnerException != null
                        ? exception.InnerException.Message
                        : "No Inner Exception";

                    var message = String.Format("Unable to finish request. Application Exception: {0}; With inner exception: {1}", exception.Message, innerExceptionMessage);
                    throw new ApplicationException(message);
                }

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                var exceptionMessage = String.Format("Unable to finish request. Server returned status: {0}; {1}", response.StatusCode, response.ReasonPhrase);
                throw new ApplicationException(exceptionMessage);
            }
        }

        public class UploadManager
        {
            CloudBlobContainer _container;
            public UploadManager(string connectionString)
            {
                _container = new CloudBlobContainer(new Uri(connectionString));
            }

            public async Task UploadStreamAsync(Stream stream, string name, int size = 8000000)
            {
                CloudBlockBlob blob = _container.GetBlockBlobReference(name);

                // local variable to track the current number of bytes read into buffer
                int bytesRead;

                // track the current block number as the code iterates through the file
                int blockNumber = 0;

                // Create list to track blockIds, it will be needed after the loop
                List<string> blockList = new List<string>();

                do
                {
                    // increment block number by 1 each iteration
                    blockNumber++;

                    // set block ID as a string and convert it to Base64 which is the required format
                    string blockId = $"{blockNumber:0000000}";
                    string base64BlockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(blockId));

                    // create buffer and retrieve chunk
                    byte[] buffer = new byte[size];
                    bytesRead = await stream.ReadAsync(buffer, 0, size);

                    // Upload buffer chunk to Azure
                    await blob.PutBlockAsync(base64BlockId, new MemoryStream(buffer, 0, bytesRead), null);

                    // add the current blockId into our list
                    blockList.Add(base64BlockId);

                    // While bytesRead == size it means there is more data left to read and process
                } while (bytesRead == size);

                // add the blockList to the Azure which allows the resource to stick together the chunks
                await blob.PutBlockListAsync(blockList);

                // make sure to dispose the stream once your are done
                stream.Dispose();
            }
        }
    }

    public static class StringExtensions
    {
        public static string EncodeToBase64String(this string original)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(original));
        }

        public static string DecodeFromBase64String(this string original)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(original));
        }
    }
}
