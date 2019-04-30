using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApps_RazorPages_Filters.Filters;

namespace WebApps_RazorPages_Filters.Pages
{
    [AddHeader("Author", "Ferdinand")]
    public class ContactModel : PageModel
    {
        private readonly ILogger _logger;

        public ContactModel(ILogger<ContactModel> logger)
        {
            _logger = logger;
        }
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            Message = "Your contact page.";
            _logger.LogDebug("Contact/OnGet");
            await Task.CompletedTask;
        }
    }
}