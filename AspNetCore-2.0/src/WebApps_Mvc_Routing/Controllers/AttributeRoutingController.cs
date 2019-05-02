using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApps_Mvc_Routing.Models;

namespace WebApps_Mvc_Routing.Controllers
{
    // Attribute routing
    public class AttributeRoutingController : Controller
    {
        [Route("")]
        [Route("AttributeRouting")]
        [Route("AttributeRouting/Index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("AttributeRouting/About")]
        public IActionResult About()
        {
            return View();
        }
        [Route("AttributeRouting/Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        // Attribute routing with Http[Verb] attributes
        [HttpGet("/products")]
        public IActionResult ListProducts()
        {
            throw new NotImplementedException();
        }

        [HttpPost("/products")]
        public IActionResult CreateProduct(ProductViewModel product)
        {
            throw new NotImplementedException();
        }

        [HttpGet("/products/{id}", Name = "Products_List")]
        public IActionResult GetProduct(int id)
        {
            throw new NotImplementedException();
        }
    }

    // Combining routes

    [Route("products")]
    public class ProductsApiController : Controller
    {
        // /products
        [HttpGet]
        public IActionResult ListProducts()
        {
            throw new NotImplementedException();
        }

        // /products/5
        [HttpGet("{id}")]
        public ActionResult GetProduct(int id)
        {
            throw new NotImplementedException();
        }
    }

    // #Token replacement in route templates ([controller], [action], [area])

    [Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
        [HttpGet] // Matches '/Products/List'
        public IActionResult List()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")] // Matches '/Products/Edit/{id}'
        public IActionResult Edit(int id)
        {
            throw new NotImplementedException();
        }
    }

    // Same as above
    public class Products1Controller : Controller
    {
        [HttpGet("[controller]/[action]")] // Matches '/Products1/List'
        public IActionResult List()
        {
            throw new NotImplementedException();
        }

        [HttpGet("[controller]/[action]/{id}")] // Matches '/Products1/Edit/{id}'
        public IActionResult Edit(int id)
        {
            throw new NotImplementedException();
        }
    }

    // With inheritance
    [Route("api/[controller]")]
    public abstract class MyBaseController : Controller {  }

    public class Products2Controller : MyBaseController
    {
        [HttpGet] // Matches '/api/Products2'
        public IActionResult List()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")] // Matches '/api/Products2/{id}'
        public IActionResult Edit(int id)
        {
            throw new NotImplementedException();
        }
    }
}