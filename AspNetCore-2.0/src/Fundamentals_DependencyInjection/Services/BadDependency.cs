using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_DependencyInjection.Services
{
    public class BadDependency
    {
        public BadDependency()
        {
        }

        public Task WriteMessage(string message)
        {
            Console.WriteLine($"MyDependency.WriteMessage called. Message: {message}");
            return Task.FromResult(0);
        }
    }
}
