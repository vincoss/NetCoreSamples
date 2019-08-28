using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApps_jQuery_Samples.Interfaces;


namespace Default_WebApplication_API_V3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IDataService _dataService;

        public ProductController(IDataService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// product/productCategory
        /// product/productCategory?term=c
        /// </summary>
        [HttpGet]
        [Route("productCategory")] 
        public IEnumerable<string> GetProductCategory(string term)
        {
            var query = _dataService.GeAdwProductCategories();

            if (string.IsNullOrWhiteSpace(term) == false)
            {
                query = query.Where(x => x != null && x.StartsWith(term, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToArray();
        }

        /// <summary>
        /// product/productSubcategory
        /// product/productSubcategory?term=c
        /// </summary>
        [HttpGet]
        [Route("productSubcategory/{id}")]
        public IEnumerable<string> GetProductSubcategory(string id, string term)
        {
            var query = (from x in _dataService.GeAdwProducts()
                        where x.ProductCategory == id
                        select x.ProductSubcategory).Distinct();

            if (string.IsNullOrWhiteSpace(term) == false)
            {
                query = query.Where(x => x != null && x.StartsWith(term, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToArray();
        }

        /// <summary>
        /// product/productSubcategory
        /// product/productSubcategory?term=c
        /// </summary>
        [HttpGet]
        [Route("productName/{id}")]
        public IEnumerable<string> GetProducts(string id, string term)
        {
            var query = (from x in _dataService.GeAdwProducts()
                         where x.ProductSubcategory == id
                         select x.Name).Distinct();

            if (string.IsNullOrWhiteSpace(term) == false)
            {
                query = query.Where(x => x != null && x.StartsWith(term, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToArray();
        }
    }
}
