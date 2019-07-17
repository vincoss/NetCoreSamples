using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor_GetStarted_Components.Data;
using Blazor_GetStarted_Components.Interfaces;


namespace Blazor_GetStarted_Components.Services
{
    public class DataAccess : IDataAccess
    {
        public DataAccess(HttpClient client)
        {
        }

        public IEnumerable<Customer> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }
    }
}