using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GettingStarted_RazorPagesContacts.Pages
{
    public class NamespacePageModel : PageModel
    {
        public NamespacePageModel()
        {
            this.Message = "Hello @namespace";
        }

        public void OnGet()
        {

        }

        public string Message { get; set; }
    }
}