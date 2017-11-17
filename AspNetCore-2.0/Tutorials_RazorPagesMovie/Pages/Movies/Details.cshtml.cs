using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tutorials_RazorPagesMovie.Models;
using Tutorials_RazorPagesMovie.Services;

namespace Tutorials_RazorPagesMovie.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly Tutorials_RazorPagesMovie.Services.MovieContext _context;

        public DetailsModel(Tutorials_RazorPagesMovie.Services.MovieContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
