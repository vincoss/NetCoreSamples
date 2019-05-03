using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApps_Mvc_DependencyInjection.Services;

namespace WebApps_Mvc_FileUploads.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDateTime _dateTime;

        // Constructor Injection
        public HomeController(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public IActionResult Index()
        {
            var serverTime = _dateTime.Now;
            if (serverTime.Hour < 12)
            {
                ViewData["Message"] = "It's morning here - Good Morning!";
            }
            else if (serverTime.Hour < 17)
            {
                ViewData["Message"] = "It's afternoon here - Good Afternoon!";
            }
            else
            {
                ViewData["Message"] = "It's evening here - Good Evening!";
            }
            return View();
        }

        // Action injection with FromServices
        public IActionResult About([FromServices] IDateTime dateTime)
        {
            ViewData["Message"] = $"Current server time: {dateTime.Now}";

            return View();
        }
    }
}