using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApps_Mvc_PartialViews.Pages
{
    public class IndexPageModel : PageModel
    {

        public void OnGet() => Page();

        public IActionResult OnGetPartial() =>
            Partial("_AuthorPartial");

    }
}