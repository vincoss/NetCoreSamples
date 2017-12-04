using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorials_RazorPagesMovie.Models;

namespace Tutorials_RazorPagesMovie.Services
{
    public interface IDatabaseService
    {

    }

    public class DatabaseService : IDatabaseService
    {
        private readonly IList<Movie> _movies;

        public DatabaseService()
        {
            _movies = new List<Movie>();
        }

        public IEnumerable<Movie> Movie
        {
            get { return _movies.ToArray(); }
        }

    }
}
