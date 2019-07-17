using Blazor_GetStarted_Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;


namespace Blazor_GetStarted_Components.Components.DependencyInjection
{
    public class CustomerListComponentBase : ComponentBase
    {
        // Dependency injection works even if using the
        // InjectAttribute in a component's base class.
        [Inject]
        protected IDataAccess DataRepository { get; set; }
    }
}
