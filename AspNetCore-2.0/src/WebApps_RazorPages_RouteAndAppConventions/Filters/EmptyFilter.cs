using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps_RazorPages_RouteAndAppConventions.Filters
{
    public class EmptyFilter : IActionFilter
    {
        public EmptyFilter()
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
