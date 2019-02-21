using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace Tutorials_RazorPagesMovieMvc.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie> Movies;
        public SelectList Genres;
        public string MovieGenre { get; set; }
        public string SearchString { get; set; }
    }
}
