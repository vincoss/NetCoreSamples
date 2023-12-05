using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;


namespace Fundamentals_AzureKeyValut_TestSamples
{
    public class MSALTests
    {
        [Fact]
        public async void T1()
        {
            var n = "";
            var k = "";
            var scopes = new[] { "User.Read" };

            string redirectUri = "https://login.windows.net";
            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(n)
                .WithClientSecret(k)
                .WithRedirectUri(redirectUri)
                .Build();

            var accounts = await app.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();
            var authResult = await app.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();
            var t = authResult?.AccessToken;
        }
    }
}
