using Blazor_GetStarted_Components.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_GetStarted_Components.Interfaces
{
    public interface IDataAccess
    {
        IEnumerable<Customer> GetAllCustomersAsync();
    }
}
