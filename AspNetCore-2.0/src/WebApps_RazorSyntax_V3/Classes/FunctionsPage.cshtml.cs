using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApps_RazorSyntax_V3.Models;


namespace WebApps_RazorSyntax_V3.Classes
{
    public class FunctionsPage : RazorPage<dynamic>
    {
        // Functions placed between here 
        public string GetHelloCodeBehind()
        {
            return $"Hello - {DateTime.Now}";
        }

        // And here.
#pragma warning disable 1998
        public override async Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div>ExecuteAsync From method: ");
            Write(GetHelloCodeBehind());
            WriteLiteral("</div>\r\n");
        }
#pragma warning restore 1998
    }
}