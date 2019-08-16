using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApps_RazorSyntax_V3.Models;

namespace WebApps_RazorSyntax_V3.Pages.Ajax
{
    public class RazorPageAjaxSamples : PageModel
    {
        public RazorPageAjaxSamples()
        {
        }

        public void OnGet()
        {

        }


        public JsonResult OnGetFilter(string search)
        {
            return new JsonResult(_filterService.GetFilterDropDownValues(filterBy));
        }
    }
}
