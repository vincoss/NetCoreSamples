using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OData_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OData_Samples.Controllers
{
    /// <summary>
    /// NOTE: Not runnable just for reference for OData all build-in conventions.
    /// </summary>
    public class AllConventionsontroller : ODataController
    {
        // GET /odata/Products
        public IQueryable<Product> Get()
        {
            throw new NotImplementedException();
        }

        // GET /odata/Products(1)
        public Product Get([FromODataUri] int key)
        {
            throw new NotImplementedException();
        }

        // GET /odata/Products(1)/ODataRouting.Models.Book
        public Book GetBook([FromODataUri] int key)
        {
            throw new NotImplementedException();
        }

        // POST /odata/Products 
        public HttpResponseMessage Post(Product item)
        {
            throw new NotImplementedException();
        }

        // PUT /odata/Products(1)
        public HttpResponseMessage Put([FromODataUri] int key, Product item)
        {
            throw new NotImplementedException();
        }

        // PATCH /odata/Products(1)
        public HttpResponseMessage Patch([FromODataUri] int key, Delta<Product> item)
        {
            throw new NotImplementedException();
        }

        // DELETE /odata/Products(1)
        public HttpResponseMessage Delete([FromODataUri] int key)
        {
            throw new NotImplementedException();
        }

        // PUT /odata/Products(1)/ODataRouting.Models.Book
        public HttpResponseMessage PutBook([FromODataUri] int key, Book item)
        {
            throw new NotImplementedException();
        }

        // PATCH /odata/Products(1)/ODataRouting.Models.Book
        public HttpResponseMessage PatchBook([FromODataUri] int key, Delta<Book> item)
        {
            throw new NotImplementedException();
        }

        // DELETE /odata/Products(1)/ODataRouting.Models.Book
        public HttpResponseMessage DeleteBook([FromODataUri] int key)
        {
            throw new NotImplementedException();
        }

        //  GET /odata/Products(1)/Supplier
        public Supplier GetSupplierFromProduct([FromODataUri] int key)
        {
            throw new NotImplementedException();
        }

        // GET /odata/Products(1)/ODataRouting.Models.Book/Author
        public Author GetAuthorFromBook([FromODataUri] int key)
        {
            throw new NotImplementedException();
        }

        // POST /odata/Products(1)/$links/Supplier
        public HttpResponseMessage CreateLink([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        {
            throw new NotImplementedException();
        }

        // DELETE /odata/Products(1)/$links/Supplier
        public HttpResponseMessage DeleteLink([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        {
            throw new NotImplementedException();
        }

        // DELETE /odata/Products(1)/$links/Parts(1)
        public HttpResponseMessage DeleteLink([FromODataUri] int key, string relatedKey, string navigationProperty)
        {
            throw new NotImplementedException();
        }

        // GET odata/Products(1)/Name
        // GET odata/Products(1)/Name/$value
        public HttpResponseMessage GetNameFromProduct([FromODataUri] int key)
        {
            throw new NotImplementedException();
        }

        // GET /odata/Products(1)/ODataRouting.Models.Book/Title
        // GET /odata/Products(1)/ODataRouting.Models.Book/Title/$value
        public HttpResponseMessage GetTitleFromBook([FromODataUri] int key)
        {
            throw new NotImplementedException();
        }
    }
}
