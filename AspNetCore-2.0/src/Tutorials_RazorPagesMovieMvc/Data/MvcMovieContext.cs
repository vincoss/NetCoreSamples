using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tutorials_RazorPagesMovieMvc.Models;

namespace Tutorials_RazorPagesMovieMvc
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Tutorials_RazorPagesMovieMvc.Models.Movie> Movie { get; set; }
    }
}
