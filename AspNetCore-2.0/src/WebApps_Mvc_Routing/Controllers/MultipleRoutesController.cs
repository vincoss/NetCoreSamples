using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApps_Mvc_Routing.Controllers
{
    // #Multiple Routes

    // Default route example
    [Route("[controller]")]
    public class MultipleRoutesController : Controller
    {
        [Route("")]     // Matches 'MultipleRoutes'
        [Route("Index")] // Matches 'MultipleRoutes/Index'
        public IActionResult Index()
        {
            return View();
        }
    }

    [Route("Store")]
    [Route("[controller]")]
    public class MultipleRoutes1Controller : Controller
    {
        [HttpPost("Buy")]     // Matches 'Products/Buy' and 'Store/Buy'
        [HttpPost("Checkout")] // Matches 'Products/Checkout' and 'Store/Checkout'
        public IActionResult Buy()
        {
            throw new NotImplementedException();
        }
    }

    [Route("api/[controller]")]
    public class Products3Controller : Controller
    {
        [HttpPut("Buy")]      // Matches PUT 'api/Products3/Buy'
        [HttpPost("Checkout")] // Matches POST 'api/Products3/Checkout'
        public IActionResult Buy()
        {
            throw new NotImplementedException();
        }
    }
}