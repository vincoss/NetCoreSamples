using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApps_RazorSyntax_V3.Pages
{
    public class FunctionsModel : RazorPage<dynamic>
    {
        public void OnGet()
        {

        }

        // Functions placed between here 
        public string GetHelloCodeBehind()
        {
            return "Hello";
        }

        // And here.
#pragma warning disable 1998
        public override async Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div>From method: ");
            Write(GetHelloCodeBehind());
            WriteLiteral("</div>\r\n");
        }
#pragma warning restore 1998
    }
}