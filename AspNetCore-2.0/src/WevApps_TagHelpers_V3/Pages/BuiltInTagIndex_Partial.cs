using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WevApps_TagHelpers_V3.Models;


namespace WevApps_TagHelpers_V3.Pages
{
    public class BuiltInTagIndex_Partial : PageModel
    {
        public BuiltInTagIndex_Partial()
        {
        }

        public Product Product { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public void OnGet()
        {
            Products = new[]
            {
                new Product
                {
                    Number = 1,
                    Name = "Test product",
                    Description = "This is a test product"
                },
                new Product
                {
                    Number = 2,
                    Name = "Test product Two",
                    Description = "This is a test product Two"
                }
            };

            Product = Products.First();
        }
    }
}
