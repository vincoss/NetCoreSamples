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
    public class IndexModel : PageModel
    {
        private readonly Tutorials_RazorPagesMovie.Services.MovieContext _context;

        public IndexModel(Tutorials_RazorPagesMovie.Services.MovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
