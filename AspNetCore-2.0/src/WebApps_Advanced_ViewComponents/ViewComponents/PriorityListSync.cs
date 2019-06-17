using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_ViewComponents.Models;

namespace WebApps_ViewComponents.ViewComponents
{
    //[ViewComponent(Name = "PriorityListSync")] // Use attribute or inherit from ViewComponent
    public class PriorityListSync : ViewComponent
    {
        private readonly ToDoContext db;

        public PriorityListSync(ToDoContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke(int maxPriority, bool isDone)
        {
            var items = new List<string> { $"maxPriority: {maxPriority}", $"isDone: {isDone}" };
            return View(items);
        }
        
    }
}