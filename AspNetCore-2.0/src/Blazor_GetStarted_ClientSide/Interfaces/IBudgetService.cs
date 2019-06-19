using Blazor_GetStarted_ClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_GetStarted_ClientSide.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<Expense>> GetExpensesAsync();
    }
}
