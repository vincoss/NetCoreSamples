using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApps_jQuery_Samples.Dto;
using WebApps_jQuery_Samples.Interfaces;
using WebApps_jQuery_Samples.Services;


namespace WebApps_jQuery_Samples.Controllers
{
    public class DataTablesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataService _dataService;

        public DataTablesController(ILogger<HomeController> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetAdwProducts()
        {
            var form = this.Request.Form;

            var draw = Request.Form["draw"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
           
            //Paging Size (10,20,50,100)    
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            int recordsFilteredTotal = 0;

            var query = from x in _dataService.GeAdwProducts() select x;
            recordsTotal = query.Count();

            // Sort
            if(string.IsNullOrWhiteSpace(sortColumn) == false && string.IsNullOrWhiteSpace(sortColumnDir) == false)
            {
                sortColumn = LinqExtensions.GetPropertyName(typeof(AdwProductDto), sortColumn);
                query = query.OrderBy(sortColumn, string.Equals("asc", sortColumnDir, StringComparison.CurrentCultureIgnoreCase));
            }

            // Search
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(m => m.Name != null && m.Name.StartsWith(searchValue, StringComparison.CurrentCultureIgnoreCase));
            }

            recordsFilteredTotal = query.Count();
            var model = query.Skip(skip).Take(pageSize).ToList();
            var response = new { draw = draw, recordsFiltered = recordsFilteredTotal, recordsTotal = recordsTotal,  data = model};

            System.Threading.Thread.Sleep(2000);

            return Json(response);
        }
    }
}
