using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData_Samples
{
    public static class Extensions
    {
        public static Task<T> AsAsync<T>(this Func<T> func)
        {
            return Task.Run<T>(func);
        }
    }
}
