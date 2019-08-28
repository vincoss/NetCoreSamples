using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApps_jQuery_Samples.Services;

namespace WebApps_jQuery_Samples.Pages
{
    public class Autocomplete : PageModel
    {
        public Autocomplete()
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
