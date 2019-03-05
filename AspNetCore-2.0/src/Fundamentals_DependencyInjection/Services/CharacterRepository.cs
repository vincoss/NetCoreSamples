using Fundamentals_DependencyInjection.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_DependencyInjection.Services
{
    public class CharacterRepository : ICharacterRepository
    {
        public Task WriteMessage(string message)
        {
            Console.WriteLine("MyDependency.WriteMessage called. Message: {0}", message);

            return Task.FromResult(0);
        }
    }
}
