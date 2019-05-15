using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApps_SessionAndAppState_V3.Services;

namespace WebApps_SessionAndAppState_V3.Pages
{
    public class IndexDpModel : PageModel
    {
        public IndexDpModel(DataService service)
        {

        }

        public void OnGet()
        {

        }
    }
}