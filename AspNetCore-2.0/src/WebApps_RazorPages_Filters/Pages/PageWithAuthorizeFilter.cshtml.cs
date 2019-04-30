using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApps_RazorPages_Filters.Pages
{
    [Authorize]
    public class PageWithAuthorizeFilterModel : PageModel
    {
        //public void OnGet()
        //{

        //}

        public IActionResult OnGet() => Page();
    }
}