using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps_RazorSyntax_V3.Pages
{
    public abstract class CustomRazorPage<TModel> : RazorPage<TModel>
    {
        public string CustomText { get; } = "Gardyloo! - A Scottish warning yelled from a window before dumping a slop bucket on the street below.";
    }
}
