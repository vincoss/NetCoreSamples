using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tutorials_ContosoUniversity_RazorPages.Models;

namespace Tutorials_ContosoUniversity_RazorPages.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly Tutorials_ContosoUniversity_RazorPages.Models.SchoolContext _context;

        public DetailsModel(Tutorials_ContosoUniversity_RazorPages.Models.SchoolContext context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student.FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
