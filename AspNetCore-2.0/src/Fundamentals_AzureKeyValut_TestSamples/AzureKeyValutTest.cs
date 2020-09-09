using System.Linq;
using System;
using Microsoft.Azure.KeyVault;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Security;
using System.Runtime.InteropServices;


namespace Fundamentals_AzureKeyValut_TestSamples
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/learn/modules/manage-secrets-with-azure-key-vault/
    /// https://docs.microsoft.com/en-us/cli/azure/storage/account?view=azure-cli-latest
    /// https://c-sharx.net/read-secrets-from-azure-key-vault-in-a-net-core-console-app
    /// </summary>
    public class AzureKeyValutTest
    {
        // This is the ID which can be found as "Application (client) ID" when selecting the registered app under "Azure Active Directory" -> "App registrations".
        const string APP_CLIENT_ID = "TODO-Client ID";

        // This is the client secret from the app registration process.
        const string APP_CLIENT_SECRET = "TODO-Secret";

        // This is available as "DNS Name" from the overview page of the Key Vault.
        const string KEYVAULT_BASE_URI = "https://TODO-blobname.vault.azure.net";

        [Fact]
        public async void GetSecretAsyncTest()
        {                         
            // Use the client SDK to get access to the key vault. To authenticate we use the identity app we registered and
            // use the client ID and the client secret to make our claim.
            var kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
                async (string authority, string resource, string scope) =>
                {
                    var authContext = new AuthenticationContext(authority);
                    var credential = new ClientCredential(APP_CLIENT_ID, APP_CLIENT_SECRET);
                    AuthenticationResult result = await authContext.AcquireTokenAsync(resource, credential);
                    if (result == null)
                    {
                        throw new InvalidOperationException("Failed to retrieve JWT token");
                    }
                    return result.AccessToken;
                }
            ));
            // Calling GetSecretAsync will trigger the authentication code above and eventually
            // retrieve the secret which we can then read.
            var secretBundle = await kvc.GetSecretAsync(KEYVAULT_BASE_URI, "ConnectionStrings--Northwind--Dev");
            Assert.NotNull(secretBundle);
        }

        [Fact]
        public async void GetSecretsAsyncTest()
        {
            // Use the client SDK to get access to the key vault. To authenticate we use the identity app we registered and
            // use the client ID and the client secret to make our claim.
            var kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(
                async (string authority, string resource, string scope) =>
                {
                    var authContext = new AuthenticationContext(authority);
                    var credential = new ClientCredential(APP_CLIENT_ID, APP_CLIENT_SECRET);
                    AuthenticationResult result = await authContext.AcquireTokenAsync(resource, credential);
                    if (result == null)
                    {
                        throw new InvalidOperationException("Failed to retrieve JWT token");
                    }
                    return result.AccessToken;
                }
            ));
           
            var result = await kvc.GetSecretsAsync(KEYVAULT_BASE_URI);

            Assert.True(result.Any());
        }

        [Fact]
        public async void HelperTest()
        {
            var cred = new AzureConfigurationService(KEYVAULT_BASE_URI, APP_CLIENT_ID, APP_CLIENT_SECRET);

            await cred.InitialiseAzure();

            Assert.True(cred.Cache.Any());
        }
    }
}

public class AzureConfigurationService
{
    private readonly string _vaultUrl;
    private readonly string _applicationId;
    private readonly string _applicationSecret;
    public readonly Dictionary<string, SecureString> Cache = new Dictionary<string, SecureString>(StringComparer.OrdinalIgnoreCase);

    public AzureConfigurationService(string vaultUrl, string applicationId, string applicationSecret)
    {
        if (string.IsNullOrWhiteSpace(vaultUrl)) throw new ArgumentNullException(nameof(vaultUrl));
        if (string.IsNullOrWhiteSpace(applicationId)) throw new ArgumentNullException(nameof(applicationId));
        if (string.IsNullOrWhiteSpace(applicationSecret)) throw new ArgumentNullException(nameof(applicationSecret));

        _vaultUrl = vaultUrl;
        _applicationId = applicationId;
        _applicationSecret = applicationSecret;
    }

    public async Task InitialiseAzure()
    {
        var client = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetAccessTokenAsync), new HttpClient());
        var secrets = await client.GetSecretsAsync(_vaultUrl);

        foreach (var item in secrets)
        {
            var secret = await GetSecretAsync(client, item.Identifier.Name);
            var ss = ToSecureString(secret);
            Cache.Add(item.Identifier.Name, ss);
        }
    }

    public async Task<string> GetSecretAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        if (Cache.TryGetValue(key, out var value))
        {
            return ToStringFromSecureString(value);
        }
        else
        {
            var client = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetAccessTokenAsync), new HttpClient());
            var secret = await GetSecretAsync(client, key);
            var ss = ToSecureString(secret);
            Cache.Add(key, ss);
            return secret;
        }
    }

    private async Task<string> GetSecretAsync(KeyVaultClient client, string key)
    {
        var secret = await client.GetSecretAsync(_vaultUrl, key);
        return secret.Value;
    }

    private async Task<string> GetAccessTokenAsync(string authority, string resource, string scope)
    {
        var appCredentials = new ClientCredential(_applicationId, _applicationSecret);
        var context = new AuthenticationContext(authority, TokenCache.DefaultShared);

        var result = await context.AcquireTokenAsync(resource, appCredentials);

        if (result == null)
        {
            throw new InvalidOperationException("Failed to retrieve JWT token");
        }

        return result.AccessToken;
    }

    private static SecureString ToSecureString(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

       var secureString = new SecureString();
        foreach (var c in value)
        {
            secureString.AppendChar(c);
        }

        return secureString;
    }

    private static string ToStringFromSecureString(SecureString value)
    {
        IntPtr valuePtr = IntPtr.Zero;
        try
        {
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
            return Marshal.PtrToStringUni(valuePtr);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        }
    }
}
