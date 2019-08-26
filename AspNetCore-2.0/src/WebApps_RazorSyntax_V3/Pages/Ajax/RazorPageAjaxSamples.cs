using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApps_RazorSyntax_V3.Models;
using WebApps_RazorSyntax_V3.Services;

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

        public async Task<IActionResult> OnGetKeywordsAll(string term)
        {
            var service = new DataService();
            var data =  await service.GetCSharpKeywords();

            if(string.IsNullOrWhiteSpace(term) == false)
            {
                data = data.Where(x => x.StartsWith(term));
            }

            return new JsonResult(data);
        }
    }
}
