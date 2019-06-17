using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_Advanced_AppModel.Conventions;

namespace WebApps_Advanced_AppModel.Controllers
{
    public class HomeController : Controller
    {
        // Route: /Home/MyCoolAction
        [CustomActionName("MyCoolAction")]
        public string SomeName()
        {
            return ControllerContext.ActionDescriptor.ActionName;
        }
    }
}
