using Blazor_GetStarted_ClientSide_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_GetStarted_ClientSide_V3.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<Expense>> GetExpensesAsync();
    }
}
