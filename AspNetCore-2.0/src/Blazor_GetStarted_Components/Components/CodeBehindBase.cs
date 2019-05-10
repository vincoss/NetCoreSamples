using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Blazor_GetStarted_Components.Components
{
    public class CodeBehindBase : ComponentBase
    {
        public string BlazorRocksText { get; set; } = "Blazor rocks the browser!";
    }
}
