using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApps_RazorSyntax_V3.Models;

namespace WebApps_RazorSyntax_V3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //home/functions
        public IActionResult Functions()
        {
            return View();
        }

        //home/inheritspage
        public IActionResult InheritsPage()
        {
            var model = new LoginViewModel
            {
                Email = "ferdinand@razorsyntax.com"
            };
            return View(model);
        }
    }
}