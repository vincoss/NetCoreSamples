using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WevApps_TagHelpers_V3.Models;
using WevApps_TagHelpers_V3.TagHelpers;

namespace WevApps_TagHelpers_V3.Pages
{
    public class RegisterComponentPageModel : PageModel
    {
        private readonly ITagHelperComponentManager _tagHelperComponentManager;

        public bool IsWeekend
        {
            get
            {
                var dayOfWeek = DateTime.Now.DayOfWeek;

                return dayOfWeek == DayOfWeek.Saturday ||
                       dayOfWeek == DayOfWeek.Sunday;
            }
        }

        public RegisterComponentPageModel(ITagHelperComponentManager tagHelperComponentManager)
        {
            _tagHelperComponentManager = tagHelperComponentManager;
        }

        public void OnGet()
        {
            string markup;

            if (IsWeekend)
            {
                markup = "<em class='text-warning'>Office closed today!</em>";
            }
            else
            {
                markup = "<em class='text-info'>Office open today!</em>";
            }

            _tagHelperComponentManager.Components.Add(new AddressTagHelperComponent(markup, 1));
        }
    }
}
