using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OData_Samples.Data;
using OData_Samples.Models;

namespace OData_Samples.Controllers
{
    /// <summary>
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
    /// 
    /// $value (Get individual property from an entity)
    /// service/Products(1)/Name
    /// service/Products(1)/Name/$value
    /// 
    /// $top
    /// service/Categories?$top=1
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
    }

    public class SuppliersController : ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(SampleData.Suppliers);
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
}