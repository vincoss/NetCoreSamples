using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tutorials_RazorPagesMovie.Models;
using Tutorials_RazorPagesMovie.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tutorials_RazorPagesMovie.Pages.Movies
{

    /// <summary>
    /// Example with search string
    /// http://localhost:53402/Movies?searchString=Ghost
    /// or if route constraint is specified @page "{searchString?}"
    /// http://localhost:53402/Movies/Ghost
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly Tutorials_RazorPagesMovie.Services.MovieContext _context;

        public IndexModel(Tutorials_RazorPagesMovie.Services.MovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }
        public SelectList Genres;
        public string MovieGenre { get; set; }

        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public async Task OnGetAsync(string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    }
}
