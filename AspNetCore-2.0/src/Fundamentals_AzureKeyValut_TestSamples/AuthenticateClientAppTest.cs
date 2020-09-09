using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xunit;


namespace Fundamentals_AzureKeyValut_TestSamples
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/rest/api/datacatalog/authenticate-a-client-app
    /// </summary>
    public class AuthenticateClientAppTest
    {
        [Fact]
        public async void Test()
        {
            var clientId = "Client ID";
            var tenantId = "Tenant ID";
            var clientSecret = "Secret";

            var authority = $"https://login.windows.net/{tenantId}";
            var resourceUri = "https://datacatalog.azure.com";

            var clientCredential = new ClientCredential(clientId, clientSecret);
            var authenticationContext = new AuthenticationContext(authority);
            var result = await authenticationContext.AcquireTokenAsync(resourceUri, clientCredential);

            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);
        }
    }
}
