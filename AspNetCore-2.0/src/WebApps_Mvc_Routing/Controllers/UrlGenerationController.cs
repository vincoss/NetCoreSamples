using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_Mvc_Routing.Models;

namespace WebApps_Mvc_Routing.Controllers
{
    // #URL Generation

    // Conventional
    public class UrlGenerationController : Controller
    {
        public IActionResult Source()
        {
            // Generates /UrlGeneration/Destination
            var url = Url.Action("Destination");
            return Content($"Go check out {url}, it's really great.");
        }

        public IActionResult Destination()
        {
            return View();
        }
    }

    // Generating url from route With attributes
    public class UrlGeneration1Controller : Controller
    {
        [HttpGet("")]
        public IActionResult Source()
        {
            var url = Url.Action("Destination"); // Generates /custom/url/to/destination
            return Content($"Go check out {url}, it's really great.");
        }

        [HttpGet("custom/url/to/destination")]
        public IActionResult Destination()
        {
            return View();
        }
    }

    // Generating url by arguments
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            // Generates /Products/Buy/17?color=red
            var url = Url.Action("Buy", "Products", new { id = 17, color = "red" });
            return Content(url);
        }
    }

    // Generating URLs by route
    public class UrlGeneration2Controller : Controller
    {
        [HttpGet("")]
        public IActionResult Source()
        {
            var url = Url.RouteUrl("Destination_Route"); // Generates /custom/url/to/destination
            return Content($"See {url}, it's really great.");
        }

        [HttpGet("custom/url/to/destination", Name = "Destination_Route")]
        public IActionResult Destination()
        {
            return View();
        }

        // Generating URLS in Action Results
        public IActionResult Edit(int id, Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Update DB with new details.
                return RedirectToAction("Index");
            }
            return View(customer);
        }
    }
}