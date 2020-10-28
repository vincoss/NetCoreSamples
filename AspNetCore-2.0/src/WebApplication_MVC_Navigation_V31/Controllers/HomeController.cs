using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication_MVC_Navigation_V31.Models;
using WebApplication_MVC_Navigation_V31.Navigation;

namespace WebApplication_MVC_Navigation_V31.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INavigationService _navigationService;

        public HomeController(ILogger<HomeController> logger, INavigationService navigationService)
        {
            _logger = logger;
            _navigationService = navigationService;
        }

        public IActionResult Index()
        {
            ViewBag.Title = AppResources.Home;

            var pages = _navigationService.GetUserAllowedPages(User);

            ViewBag.Navigation = pages;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SidebarOne()
        {
            return View();
        }
    }
}
