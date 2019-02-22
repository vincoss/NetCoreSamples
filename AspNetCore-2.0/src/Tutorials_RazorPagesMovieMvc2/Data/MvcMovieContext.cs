using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tutorials_RazorPagesMovieMvc2.Models;

namespace Tutorials_RazorPagesMovieMvc2
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Tutorials_RazorPagesMovieMvc2.Models.Movie> Movie { get; set; }
    }
}
