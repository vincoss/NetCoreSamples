using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GettingStarted_RazorPagesContacts.Services;
using GettingStarted_RazorPagesContacts.Domain;

namespace GettingStarted_RazorPagesContacts.Pages.Customer
{
    public class CreateModel : PageModel
    {
        private readonly IDatabaseService _databaseService;

        public CreateModel(IDatabaseService databaseService)
        {
            if(databaseService == null)
            {
                throw new ArgumentNullException(nameof(databaseService));
            }
            _databaseService = databaseService;
        }

        //public void OnGet()
        //{

        //}

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public CustomerInfo Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _databaseService.AddCustomerAsync(Customer);
            Message = $"Customer {Customer.FirstName} {Customer.LastName} added";
            return RedirectToPage("./Index");
        }
    }
}