using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fundamentals_DependencyInjection.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fundamentals_DependencyInjection.Pages
{
    public class BadDependencyPageModel : PageModel
    {
        public BadDependency _dependency = new BadDependency();

        public async Task OnGet()
        {
            await _dependency.WriteMessage("IndexModel.OnGetAsync created this message.");
        }
    }
}