using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApps_ViewComponents.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApps_ViewComponents.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoContext _ToDoContext;

        public ToDoController(ToDoContext context)
        {
            _ToDoContext = context;

            // EnsureCreated() is used to call OnModelCreating for In-Memory databases as migration is not possible
            // see: https://github.com/aspnet/EntityFrameworkCore/issues/11666 
            _ToDoContext.Database.EnsureCreated();
        }

        public IActionResult Index()
        {
            var model = _ToDoContext.ToDo.ToList();
            return View(model);
        }

        /// <summary>
        /// Invoking a view component directly from a controller
        /// </summary>
        public IActionResult IndexVC()
        {
            return ViewComponent("PriorityList", new { maxPriority = 3, isDone = false });
        }

        public async Task<IActionResult> IndexFinal()
        {
            return View(await _ToDoContext.ToDo.ToListAsync());
        }

        public IActionResult IndexNameof()
        {
            return View(_ToDoContext.ToDo.ToList());
        }

        public IActionResult IndexFirst()
        {
            return View(_ToDoContext.ToDo.ToList());
        }

        public IActionResult IndexModel()
        {
            return View(_ToDoContext.ToDo.ToList());
        }

        public IActionResult PriorityListSync()
        {
            return View(_ToDoContext.ToDo.ToList());
        }
    }
}
