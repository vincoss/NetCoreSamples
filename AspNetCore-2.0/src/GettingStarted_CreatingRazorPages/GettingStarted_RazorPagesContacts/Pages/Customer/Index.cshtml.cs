using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GettingStarted_RazorPagesContacts.Domain;
using GettingStarted_RazorPagesContacts.Services;


namespace GettingStarted_RazorPagesContacts.Pages.Customer
{
    public class IndexModel : PageModel
    {
        private readonly IDatabaseService _databaseService;

        public IndexModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }

        public IEnumerable<CustomerInfo> Customers { get; private set; }


        //public void OnGet()
        //{

        //}

        [TempData]
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            Customers = await _databaseService.Customers.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var contact = await _databaseService.FindCustomerAsync(id);

            if (contact != null)
            {
               await  _databaseService.RemoveAsync(contact);
            }

            return RedirectToPage();
        }
    }
}