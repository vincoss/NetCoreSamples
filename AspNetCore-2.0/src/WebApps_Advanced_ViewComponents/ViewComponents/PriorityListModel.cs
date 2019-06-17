using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_ViewComponents.Models;

namespace WebApps_ViewComponents.ViewComponents
{
    //[ViewComponent(Name = "PriorityListModel")] // Use attribute or inherit from ViewComponent
    public class PriorityListModel : ViewComponent
    {
        public PriorityListModel()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<TodoItem> modelData)
        {
            await Task.Delay(5000);
            return View(modelData);
        }
    }
}