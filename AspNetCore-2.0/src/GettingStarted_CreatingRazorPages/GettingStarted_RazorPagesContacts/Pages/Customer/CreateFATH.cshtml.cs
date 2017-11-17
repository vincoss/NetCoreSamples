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
    public class CreateFATHModel : PageModel
    {
        private readonly IDatabaseService _databaseService;

        public CreateFATHModel(IDatabaseService databaseService)
        {
            if (databaseService == null)
            {
                throw new ArgumentNullException(nameof(databaseService));
            }
            _databaseService = databaseService;
        }

        [BindProperty]
        public CustomerInfo Customer { get; set; }

        public async Task<IActionResult> OnPostJoinListAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _databaseService.AddCustomerAsync(Customer);
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostJoinListUCAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Customer.FirstName = Customer.FirstName?.ToUpper();
            return await OnPostJoinListAsync();
        }
    }
}