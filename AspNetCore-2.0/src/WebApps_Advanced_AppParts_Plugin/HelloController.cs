using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApps_Advanced_AppParts_Plugin
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Hello from a plugin assembly!");
        }
    }
}
