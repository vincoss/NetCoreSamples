using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Data_OData_Sample.Data
{
    public class AdventureWorks2016Context : DbContext
    {
        public AdventureWorks2016Context(DbContextOptions<AdventureWorks2016Context> options) : base(options)
        {
        }

        public DbSet<string> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<string>().ToTable("Course");
        }
    }
}
