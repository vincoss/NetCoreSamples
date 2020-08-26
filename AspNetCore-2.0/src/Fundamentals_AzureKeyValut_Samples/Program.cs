using System.Linq;
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;


namespace Fundamentals_AzureKeyValut_Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
         CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((context, config) =>
                 {
                     if (context.HostingEnvironment.IsProduction())
                     {
                        var builtConfig = config.Build();

                         var azureServiceTokenProvider = new AzureServiceTokenProvider();
                         var keyVaultClient = new KeyVaultClient(
                             new KeyVaultClient.AuthenticationCallback(
                                 azureServiceTokenProvider.KeyVaultTokenCallback));

                         config.AddAzureKeyVault(
                             $"https://{builtConfig["KeyVaultName"]}.vault.azure.net/",
                             keyVaultClient,
                             new DefaultKeyVaultSecretManager());
                     }
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
