using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Fundamentals_HandleErrors.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Security_WS_Federation.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            // Disable status code pages

            var statusCodePagesFeature = HttpContext.Features.Get<IStatusCodePagesFeature>();

            if (statusCodePagesFeature != null)
            {
                statusCodePagesFeature.Enabled = false;
            }
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
