using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_DependencyInjection.Interfaces
{
    public interface ICharacterRepository
    {
        Task WriteMessage(string message);
    }
}
