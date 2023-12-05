using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.KeyVault;
using Azure.ResourceManager.KeyVault.Models;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using Azure.Security.KeyVault.Secrets;
using Azure_ConsoleTest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using NSubstitute;
using System;
using System.Collections.Concurrent;
using System.Runtime;
using System.Security;
using System.Text;

namespace MyApp // Note: actual namespace depends on the project name.
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			 await ListSecreets();

			Console.WriteLine("Done...");
		}

		public static async Task ListSecreets()
		{
			var logger = new NullLogger<AzureConfigurationService>();
			var configureation = Substitute.For<IConfiguration>();
			var cred = new AzureConfigurationService(TestAzureHelper.AzureOptions, logger);

			await cred.InitialiseAzure();

			var sb = new StringBuilder();

			foreach (var key in cred.GetKeys().OrderBy(x => x))
			{
				sb.AppendLine($"{key} - {await cred.GetSecretAsync(key)}");
			}

			File.WriteAllText(@$"C:\Temp\{nameof(ListSecreets)}.txt", sb.ToString());
		}

		public static async Task DefaultAzureCredentialTest()
		{
			// When deployed to an azure host, the default azure credential will authenticate the specified user assigned managed identity.
			string userAssignedClientId = "9dfeba6c-95e9-4b42-b114-599f80acdd13";
			//var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = userAssignedClientId });
			var credential = new ManagedIdentityCredential();

			ArmClient client = new ArmClient(credential);


			var sub = client.GetDefaultSubscription();
			Console.WriteLine(sub.Id);

			Console.WriteLine("GetSubscriptions");
			var subscriptions =  client.GetSubscriptions();
			foreach (var subscription in subscriptions)
			{
				Console.WriteLine(subscription.Id);
				if(subscription.Data != null)
				{
					Console.WriteLine(subscription.Data.AuthorizationSource);
					Console.WriteLine(subscription.Data.SubscriptionId);
					Console.WriteLine(subscription.Data.Id);
					Console.WriteLine(subscription.Data.TenantId);
					Console.WriteLine(subscription.Data.Tags);
					Console.WriteLine(subscription.Data.DisplayName);
				}

				Console.WriteLine("GetKeyVaultsAsync");
				var vaults = subscription.GetKeyVaultsAsync();
				await foreach(var vault in vaults)
				{
					Console.WriteLine(vault.Id);

					if(vault.Data != null)
					{
						Console.WriteLine(vault.Data.Name);
					}
				}

				Console.WriteLine("GetStorageAccountsAsync");
				var storageAccounts = subscription.GetStorageAccountsAsync();

				await foreach(var storage in storageAccounts)
				{
					Console.WriteLine(storage.Id);
					if (storage.Data != null)
					{
						Console.WriteLine(storage.Data.Name);
					}

					Console.WriteLine("GetKeysAsync");
					var storageKeys = storage.GetKeysAsync();

					await foreach(var storageKey in storageKeys)
					{
						Console.WriteLine(storageKey.Value);
					}
				}

			}

			Console.WriteLine("Tennants");
			var tenants = client.GetTenants();
			foreach (var tenant in tenants)
			{
				Console.WriteLine(tenant.Id);
			}

			


			//var defaultSubscription = await client.GetDefaultSubscriptionAsync();

			//var resourceGroupName = "";
			//ResourceIdentifier resourceGroupResourceId = ResourceGroupResource.CreateResourceIdentifier(defaultSubscription.Id, resourceGroupName);
			//ResourceGroupResource resourceGroupResource = client.GetResourceGroupResource(resourceGroupResourceId);

			//KeyVaultCollection collection = resourceGroupResource.GetKeyVaults();

			//foreach (var keyVault in collection)
			//{
			//	Console.WriteLine(keyVault.Id);
			//}


		}

		/// <summary>
		/// https://learn.microsoft.com/en-us/azure/key-vault/secrets/quick-create-net?tabs=azure-cli
		/// </summary>
		/// <returns></returns>
		public static async Task Test()
		{
			var clientId = "";
			var tenantId = "";
			var clientSecret = "";


			string keyVaultName = "";
			var kvUri = "https://" + keyVaultName + ".vault.azure.net";

			var client = new SecretClient(new Uri(kvUri), new ClientSecretCredential(tenantId, clientId, clientSecret));

			//var cred = new ClientSecretCredential(tenantId, clientId, clientSecret);
			
			//var client = new SecretClient(new Uri(kvUri, UriKind.Relative), new ClientSecretCredential(tenantId, clientId, clientSecret));

			var secreets = client.GetPropertiesOfSecretsAsync();

			await foreach (var item in secreets)
			{
				Console.WriteLine(item.Name);

				var sec = await client.GetSecretAsync(item.Name);

				Console.WriteLine(sec.Value.Value);
			}
		}

		private static void SampleVault()
		{
			var credential = GetCredential();
			var secretClient = GetValut(credential, null);

			Console.WriteLine(secretClient.VaultUri);
		}





		public static TokenCredential GetCredential()
		{
			var clientId = "9dfeba6c-95e9-4b42-b114-599f80acdd13";
			var tenantId = "3721078a-9a5b-472f-b173-637fce1dee74";
			var clientSecret = "xeTTfBk-1Vl0yI.7r-B841Mf8r5_S.5RH_";
			tenantId = "";

			/*
                NOTE: If runned locally need to provide azure cred to authenticate since not running under azure environment. 
            */

			TokenCredential? credentials = null;

			if (string.IsNullOrWhiteSpace(tenantId))
			{
				credentials = new ManagedIdentityCredential();
			}
			else
			{
				credentials = new ClientSecretCredential(tenantId, clientId, clientSecret);
			}

			return credentials;
		}

		private SecretClient GetValut()
		{
			var credential = GetCredential();
			var secretClient = GetValut(credential, null);
			return secretClient;
		}

		private StorageAccountResource GetStorageAccount(string storageName)
		{
			var credential = GetCredential();
			var storageAccount = GetStorageAccount(credential, storageName);
			return storageAccount;
		}

		private static SecretClient GetValut(TokenCredential tokenCredential, string? vaultName)
		{
			var client = new ArmClient(tokenCredential);
			var subscription = client.GetDefaultSubscription();
			var vaults = subscription.GetKeyVaults();

			if (vaults.Any() == false)
			{
				throw new InvalidOperationException($"There are not vaults for default subscription: {subscription.Id}");
			}

			SecretClient? secretClient = null;
			var template = "https://{0}.vault.azure.net";
			vaultName = vaultName?.ToLower();

			if (string.IsNullOrWhiteSpace(vaultName))
			{
				var kvUri = string.Format(template, vaults.First().Data.Name);
				secretClient = new SecretClient(new Uri(kvUri), tokenCredential);
			}
			else
			{
				var v = vaults.Where(x => x.Data.Name == vaultName).FirstOrDefault();
				if(v != null)
				{
					var kvUri = string.Format(template, v.Data.Name);
					secretClient = new SecretClient(new Uri(kvUri), tokenCredential);
				}
			}

			if (secretClient == null)
			{
				throw new InvalidOperationException($"Could not find valut: {vaultName} for default subscription: {subscription.Id}");
			}

			return secretClient;
		}

		private static StorageAccountResource GetStorageAccount(TokenCredential tokenCredential, string? storageName)
		{
			if (string.IsNullOrWhiteSpace(storageName)) throw new ArgumentNullException(nameof(storageName));

			var client = new ArmClient(tokenCredential);
			var subscription = client.GetDefaultSubscription();
			var storages = subscription.GetStorageAccounts();
			storageName  = storageName?.ToLower();

			if (storages.Any() == false)
			{
				throw new InvalidOperationException($"There are not storage accounts for default subscription: {subscription.Id}");
			}

			var account = storages.Where(x => x.Data.Name == storageName).FirstOrDefault();

			if (account == null)
			{
				throw new InvalidOperationException($"Could not find storage account: {storageName} for default subscription: {subscription.Id}. Please review permissions for managed identity on an existing VM. ");
			}

			return account;
		}

		private string GetSecretAsync(SecretClient vault, string key)
		{
			var secret = vault.GetSecret(key);
			return secret.Value.Value;
		}

		public string GetStorageKey( string storageName)
		{
			//if (string.IsNullOrWhiteSpace(storageName)) throw new ArgumentNullException(nameof(storageName));

			//storageName = storageName.ToLower();

			//var secure = Cache.GetOrAdd(storageName, (key) =>
			//{
			//	//Validate();

			//	var account = GetStorageAccount(storageName);

			//	var sk = account.GetKeys().First().Value;
			//	var ss = sk.ToSecureString();
			//	return ss;
			//});

			//return secure.ToStringFromSecureString();

			throw new NotImplementedException();
		}


		private readonly ConcurrentDictionary<string, SecureString> Cache = new ConcurrentDictionary<string, SecureString>(StringComparer.OrdinalIgnoreCase);

		////public Task<string> GetSecretAsync(string key)
		////{
		////	if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

		////	key = key.ToLower();

		////	var secure = Cache.GetOrAdd(key, (key) =>
		////	{
		////		//Validate();
		////		var valut = GetValut();
		////		var secret = GetSecretAsync(valut, key);
		////		var ss = secret.ToSecureString();
		////		return ss;
		////	});

		////	return Task.FromResult(secure.ToStringFromSecureString());
		////}

	}
}