﻿using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Fundamentals_AzureKeyValut_TestSamples
{
	public class MicrosoftAzureResourceManagerTest
	{
		public async Task DefaultAzureCredentialTest()
		{
			// When deployed to an azure host, the default azure credential will authenticate the specified user assigned managed identity.
			string userAssignedClientId = "9dfeba6c-95e9-4b42-b114-599f80acdd13";
			var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = userAssignedClientId });
		}

			[Fact]
		public async Task EnvironmentCredentialTest()
		{
			var clientId = "9dfeba6c-95e9-4b42-b114-599f80acdd13";
			var tenantId = "3721078a-9a5b-472f-b173-637fce1dee74";
			var clientSecret = "xeTTfBk-1Vl0yI.7r-B841Mf8r5_S.5RH_";

			// First we construct our client
			ArmClient client = new ArmClient(new EnvironmentCredential());

			//// Next we get a resource group object
			//// ResourceGroupResource is a [Resource] object from above
			//SubscriptionResource subscription = await client.GetDefaultSubscriptionAsync();
			//ResourceGroupCollection resourceGroups = subscription.GetResourceGroups();
			//ResourceGroupResource resourceGroup = await resourceGroups.GetAsync("myRgName");

			//// Next we get the collection for the virtual machines
			//// vmCollection is a [Resource]Collection object from above
			//VirtualMachineCollection virtualMachines = resourceGroup.GetVirtualMachines();

			//// Next we loop over all vms in the collection
			//// Each vm is a [Resource] object from above
			//await foreach (VirtualMachineResource virtualMachine in virtualMachines)
			//{
			//	// We access the [Resource]Data properties from vm.Data
			//	if (!virtualMachine.Data.Tags.ContainsKey("owner"))
			//	{
			//		// We can also access all operations from vm since it is already scoped for us
			//		await virtualMachine.AddTagAsync("owner", "tagValue");
			//	}
			//}
		}
	}
}