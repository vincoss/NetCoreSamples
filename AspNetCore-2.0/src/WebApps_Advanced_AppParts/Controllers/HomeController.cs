using WebApps_Advanced_AppParts.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebApps_Advanced_AppParts.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var entities = EntityTypes.Types;
            return View(entities);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
