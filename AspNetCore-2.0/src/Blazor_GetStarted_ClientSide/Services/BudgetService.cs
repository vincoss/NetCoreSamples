using Blazor_GetStarted_ClientSide.Interfaces;
using Blazor_GetStarted_ClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_GetStarted_ClientSide.Services
{
    public class BudgetService : IBudgetService
    {
        public Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            var items = new List<Expense>();
            items.Add(new Expense { Description = "Milk", Amount = 2.0M });
            items.Add(new Expense { Description = "Bread", Amount = 2.5M });
            items.Add(new Expense { Description = "Pork Shoulder", Amount = 22.5M });

            return Task.Factory.StartNew(() =>
            {
                Task.Delay(5000);
                return items.AsEnumerable();
            });
        }
    }
}
