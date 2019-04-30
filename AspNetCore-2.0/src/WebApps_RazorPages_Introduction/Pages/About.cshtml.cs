using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApps_RazorPages_Introduction.Pages
{
    public class AboutModel : PageModel
    {
        [ViewData]
        public string Title { get; } = "About";

        public void OnGet()
        {
        }
    }
}