using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps_RazorPages_RouteAndAppConventions.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ReplaceRouteValueFilterAttribute : Attribute, IPageFilter
    {
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            // Called after the handler method executes before the result.
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            // Called before the handler method executes after model binding is complete.
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            // Called after a handler method is selected but before model binding occurs.
            context.RouteData.Values.TryGetValue("globalTemplate",
                out var globalTemplateValue);
            if (string.Equals((string)globalTemplateValue, "TriggerValue",
                StringComparison.Ordinal))
            {
                context.RouteData.Values["globalTemplate"] = "ReplacementValue";
            }
        }
    }
}
