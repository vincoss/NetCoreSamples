using OData_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData_Samples.Data
{
    public static class SampleData
    {
        public static readonly ICollection<Keyword> Keywords = new HashSet<Keyword>();
        public static readonly ICollection<Variable> Variables = new HashSet<Variable>();

        static SampleData()
        {
            // Keywords
            Keywords.Add(new Keyword { Name = "abstract", Weight = 1.1M, ReleaseDate = new DateTime(2005, 11, 01) });
            Keywords.Add(new Keyword { Name = "as", Weight = 1.2M, ReleaseDate = new DateTime(2005, 12, 01) });
            Keywords.Add(new Keyword { Name = "base", Weight = 1.3M, ReleaseDate = new DateTime(2006, 11, 01) });
            Keywords.Add(new Keyword { Name = "bool", Weight = 1.4M, ReleaseDate = new DateTime(2007, 07, 01) });
            Keywords.Add(new Keyword { Name = "break", Weight = 1.5M, ReleaseDate = new DateTime(2008, 01, 01) });


            Keywords.Add(new Keyword { Name = "byte", Weight = 1.6M, ReleaseDate = new DateTime(2008, 01, 01) });
            Keywords.Add(new Keyword { Name = "case", Weight = 1.7M, ReleaseDate = new DateTime(2008, 01, 01) });
            Keywords.Add(new Keyword { Name = "catch", Weight = 1.8M, ReleaseDate = new DateTime(2008, 01, 01) });
            Keywords.Add(new Keyword { Name = "char", Weight = 1.9M, ReleaseDate = new DateTime(2008, 01, 01) });
            Keywords.Add(new Keyword { Name = "checked", Weight = 2.0M, ReleaseDate = new DateTime(2008, 01, 01) });
            Keywords.Add(new Keyword { Name = "class", Weight = 2.1M, ReleaseDate = new DateTime(2008, 01, 01) });
            Keywords.Add(new Keyword { Name = "const", Weight = 2.2M, ReleaseDate = new DateTime(2008, 01, 01) });

            // Variables
            Variables.Add(new Variable { Name = "abstract class", Keyword = Keywords.Single(x => x.Name == "abstract") });
            Variables.Add(new Variable { Name = "convert as some type", Keyword = Keywords.Single(x => x.Name == "as") });
            Variables.Add(new Variable { Name = "base type", Keyword = Keywords.Single(x => x.Name == "base") });
        }

    }
}
