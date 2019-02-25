using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tutorials_ContosoUniversity_RazorPages.Models;

namespace Tutorials_ContosoUniversity_RazorPages.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly Tutorials_ContosoUniversity_RazorPages.Models.SchoolContext _context;

        public CreateModel(Tutorials_ContosoUniversity_RazorPages.Models.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Student.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}