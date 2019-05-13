using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_GetStarted_Components.wwwroot
{
    public static class Extensions
    {
        public static Task Focus(this ElementRef elementRef, IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<object>("exampleJsFunctions.focusElement", elementRef);
        }
    }
}
