using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_GetStarted_Components.Components.DependencyInjection
{
    public class CustomerListComponentBase : IComponent
    {
        // Dependency injection works even if using the
        // InjectAttribute in a component's base class.
        [Inject]
        protected IDataAccess DataRepository { get; set; }
    }
}
