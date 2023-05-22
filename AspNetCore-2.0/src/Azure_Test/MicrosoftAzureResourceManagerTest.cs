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
		[Fact]
		public async Task Test()
		{
			//// First we construct our client
			//ArmClient client = new ArmClient(new DefaultAzureCredential());

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
