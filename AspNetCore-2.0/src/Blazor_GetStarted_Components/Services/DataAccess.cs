using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor_GetStarted_Components.Interfaces;


namespace Blazor_GetStarted_Components.Services
{
    public class DataAccess : IDataAccess
    {
        public DataAccess(HttpClient client)
        {
    }
    }
}
