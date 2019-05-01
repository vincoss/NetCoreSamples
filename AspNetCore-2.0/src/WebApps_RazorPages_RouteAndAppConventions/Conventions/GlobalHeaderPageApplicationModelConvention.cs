using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_RazorPages_RouteAndAppConventions.Filters;

namespace WebApps_RazorPages_RouteAndAppConventions.Conventions
{
    public class GlobalHeaderPageApplicationModelConvention : IPageApplicationModelConvention
    {
        public void Apply(PageApplicationModel model)
        {
            model.Filters.Add(new AddHeaderAttribute(
                "GlobalHeader", new string[] { "Global Header Value" }));
        }
    }
}
