using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApps_jQuery_Samples.Services;

namespace WebApps_jQuery_Samples.Areas.ChartJs.Pages
{
    public class PieChart0001 : PageModel
    {
        public PieChart0001()
        {
        }

        public string ChartData { get; set; }

        public void OnGet()
        {
            var data = new
            {
                datasets = new[]
                {
                   new
                   {
                       data = new[] { 10, 20, 30 },
                       backgroundColor = new[] { "#ff0000", "#0099ff", "#ffff00" }
                   }
                },

                labels = new[] { "Red", "Blue", "Yellow" }
            };

            ChartData = JsonConvert.SerializeObject(data);
        }
    }
}
