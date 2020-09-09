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

            var azure = Azure
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
    }
}
