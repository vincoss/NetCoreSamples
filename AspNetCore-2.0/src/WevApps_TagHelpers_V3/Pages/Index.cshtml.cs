using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WevApps_TagHelpers_V3.Models;


namespace WevApps_TagHelpers_V3.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
            Movie = new Movie
            {
                ID = 1,
                Title = "Train to Busan",
                ReleaseDate = new DateTime(2017, 1, 1),
                Genre = "Horror",
                Price = 10.50M
            };

            Email = "taghelpers@aspnetcore.com";
        }

        public void OnGet()
        {

        }

        public Movie Movie { get; set; }

        public string Email { get; set; }
    }
}
