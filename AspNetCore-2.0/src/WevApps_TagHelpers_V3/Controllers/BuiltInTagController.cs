using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WevApps_TagHelpers_V3.Controllers
{
    public class BuiltInTagController : Controller
    {
        public IActionResult Index()
        {
            var speaker = new Speaker
            {
                SpeakerId = 12
            };
            return View(speaker);
        }

        public IActionResult AnchorTagHelper(int id)
        {
            var speaker = new Speaker
            {
                SpeakerId = id
            };

            return View(speaker);
        }
    }
}
