using OData_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData_Samples.Data
{
    public static class SampleData
    {
        static SampleData()
        {
            LoadKeywordData();
            LoadProductData();
        }

        #region Keyword data

        public static readonly ICollection<Keyword> Keywords = new HashSet<Keyword>();
        public static readonly ICollection<Variable> Variables = new HashSet<Variable>();

        public static void LoadKeywordData()
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

        #endregion

        #region Product data

        public static readonly ICollection<Product> Products = new HashSet<Product>();
        public static readonly ICollection<Supplier> Suppliers = new HashSet<Supplier>();
        public static readonly ICollection<Category> Categories = new HashSet<Category>();

        private static void LoadProductData()
        {
            var c1 = new Category { ID = 1, Name = "Beverages" };
            var c2 = new Category { ID = 2, Name = "Condiments" };
            var c3 = new Category { ID = 3, Name = "Confections" };

            Categories.Add(c1);
            Categories.Add(c2);
            Categories.Add(c3);

            var s1 = new Supplier { Key = "1", Name = "Exotic Liquids" };
            var s2 = new Supplier { Key = "2", Name = "New Orleans Cajun Delights" };
            var s3 = new Supplier { Key = "3", Name = "Grandma Kelly's Homestead" };

            Suppliers.Add(s1);
            Suppliers.Add(s2);
            Suppliers.Add(s3);

            var p1 = new Product { ID = 1, Name = "Chai", Price = 18.00M, CategoryId = 1, SupplierId = "1", Supplier = s1, Category = c1 };
            var p2 = new Product { ID = 2, Name = "Aniseed Syrup", Price = 10.00M, CategoryId = 1, SupplierId = "2", Supplier = s2, Category = c1 };
            var p3 = new Product { ID = 3, Name = "Grandma's Boysenberry Spread", Price = 25.00M, CategoryId = 3, SupplierId = "2", Supplier = s2, Category = c3 };

            c1.Products.Add(p1);
            c1.Products.Add(p2);
            c3.Products.Add(p3);

            Products.Add(p1);
            Products.Add(p2);
            Products.Add(p3);


        }

        #endregion
    }
}
