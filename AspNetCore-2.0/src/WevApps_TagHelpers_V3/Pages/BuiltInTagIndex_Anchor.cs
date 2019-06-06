using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WevApps_TagHelpers_V3.Models;


namespace WevApps_TagHelpers_V3.Pages
{
    public class BuiltInTagIndex_Anchor : PageModel
    {
        public BuiltInTagIndex_Anchor()
        {
        }

        public void OnGet()
        {

        }

        public void OnGetProfile(int attendeeId)
        {
            ViewData["AttendeeId"] = attendeeId;

            // code omitted for brevity
        }
    }
}
