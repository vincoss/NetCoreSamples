using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using OData_Samples.Data;
using OData_Samples.Models;

namespace OData_Samples.Controllers
{
    /// <summary>
    /// 
    /// Metadata
    /// service/$metadata
    /// 
    /// $expand
    /// service/Categories?$expand=Products
    /// service/Products(1)?$expand=Category,Supplier
    /// service/Categories(1)?$expand=Products($expand=Supplier)
    /// 
    /// $select
    /// service/Products?$select=Price,Name
    /// service/Products?$select=Name,Supplier&$expand=Supplier
    /// service/Categories?$expand=Products($select=Name)&$select=Name,Products
    /// service/Categories(1)/Products?$top=1&$orderby=Name
    /// service/Categories?$top=1&$select=Name
    /// service/Products(1)/Supplier
    /// service/Suppliers(2)/Products
    /// 
    /// $value (Get individual property from an entity)
    /// service/Products(1)/Name
    /// service/Products(1)/Name/$value
    /// 
    /// $top
    /// service/Categories?$top=1
    /// 
    /// Action
    /// service/Products(1)/ProductService.Rate?Rating=5
    /// 
    /// Function
    /// service/Products/ProductService.MostExpensive
    /// service/GetSalesTaxRate(PostalCode=10)
    /// 
    /// </summary>
    public class ProductsController : ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(SampleData.Products);
        }

        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(SampleData.Products.AsQueryable().Where(x => x.ID == key));
        }

        [EnableQuery]
        public async Task<IActionResult> GetName([FromODataUri] int key)
        {
            Product product = await Extensions.AsAsync<Product>(() =>
            {
                return SampleData.Products.AsQueryable().SingleOrDefault(x => x.ID == key);
            });
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.Name);
        }

        [EnableQuery]
        public SingleResult<Supplier> GetSupplier([FromODataUri] int key)
        {
            var result = SampleData.Products.AsQueryable().Where(m => m.ID == key).Select(m => m.Supplier);
            return SingleResult.Create(result);
        }

        // TODO: Does not work
        [HttpPost]
        public async Task<IActionResult> Rate([FromODataUri] int key, [FromBody] ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            int rating = (int)parameters["Rating"];

            await Task.Run(() =>
            {
                SampleData.Ratings.Add(new ProductRating
                {
                    ProductID = key,
                    Rating = rating
                });
            });
            
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpGet]
        public IActionResult MostExpensive()
        {
            var product = SampleData.Products.Max(x => x.Price);
            return Ok(product);
        }

        [HttpGet]
        [ODataRoute("GetSalesTaxRate(PostalCode={postalCode})")]
        public IActionResult GetSalesTaxRate([FromODataUri] int postalCode)
        {
            double rate = 5.6;  // Use a fake number for the sample.
            return Ok(rate);
        }
    }

    public class SuppliersController : ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(SampleData.Suppliers);
        }

        [EnableQuery]
        public IQueryable<Product> GetProducts([FromODataUri] int key)
        {
            return SampleData.Suppliers.AsQueryable().Where(m => m.Key == key).SelectMany(m => m.Products);
        }
    }

    public class CategoriesController : ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(SampleData.Categories);
        }

        [EnableQuery(MaxExpansionDepth = 4)]
        public IQueryable<Category> GetCategories()
        {
            return SampleData.Categories.AsQueryable();
        }

        [EnableQuery]
        public SingleResult<Category> GetCategory([FromODataUri] int key)
        {
            return SingleResult.Create(SampleData.Categories.AsQueryable().Where(x => x.ID == key));
        }
    }

    public class RatingsController : ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(SampleData.Ratings);
        }
    }
}