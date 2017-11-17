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
    public class EditModel : PageModel
    {
        private readonly IDatabaseService _databaseService;

        public EditModel(IDatabaseService databaseService)
        {
            if (databaseService == null)
            {
                throw new ArgumentNullException(nameof(databaseService));
            }
            _databaseService = databaseService;
        }

        [BindProperty]
        public CustomerInfo Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer = await _databaseService.FindCustomerAsync(id);

            if (Customer == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            try
            {
                await _databaseService.UpdateCustomerAsync(Customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"Customer {Customer.Id} not found!");
            }

            return RedirectToPage("/Index");
        }
    }
}