using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tutorials_RazorPagesMovie.Models;
using Tutorials_RazorPagesMovie.Services;

namespace Tutorials_RazorPagesMovie.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly Tutorials_RazorPagesMovie.Services.MovieContext _context;

        public CreateModel(Tutorials_RazorPagesMovie.Services.MovieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}