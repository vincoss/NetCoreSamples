using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApps_Mvc_Routing.Controllers
{
    public class AttributeRoutingOtherController : Controller
    {
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult MyIndex()
        {
            return View("Index");
        }
        [Route("Home/About")]
        public IActionResult MyAbout()
        {
            return View("About");
        }
        [Route("Home/Contact")]
        public IActionResult MyContact()
        {
            return View("Contact");
        }
    }
}