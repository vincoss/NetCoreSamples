using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps_Advanced_AppModel.Controllers
{
    public class AppModelController : Controller
    {
        public string Description()
        {
            return "Description: " + ControllerContext.ActionDescriptor.Properties["description"];
        }
    }
}
