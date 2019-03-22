using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OData_Samples.Models;

namespace OData_Samples.Controllers
{
    /// <summary>
    /// service/$metadata#Books/$entity
    /// 
    /// service/Books('978-0-7356-7942-9')
    /// </summary>
    namespace MyApp.Controllers
    {
        public class BooksController : ODataController
        {
            private IList<BookOt> _books = new List<BookOt>
            {
                new BookOt
                {
                    ISBN = "978-0-7356-8383-9",
                    Title = "SignalR Programming in Microsoft ASP.NET",
                    Press = new PressOt
                    {
                        Name = "Microsoft Press",
                        Category = CategoryOt.Book
                    }
                },

                new BookOt
                {
                    ISBN = "978-0-7356-7942-9",
                    Title = "Microsoft Azure SQL Database Step by Step",
                    Press = new PressOt
                    {
                        Name = "Microsoft Press",
                        Category = CategoryOt.EBook,
                        DynamicProperties = new Dictionary<string, object>
                        {
                            { "Blog", "http://blogs.msdn.com/b/microsoft_press/" },
                            { "Address", new AddressOt
                            {
                                  City = "Redmond", Street = "One Microsoft Way" }
                            }
                        }
                    },
                    Properties = new Dictionary<string, object>
                    {
                        { "Published", new DateTimeOffset(2014, 7, 3, 0, 0, 0, 0, new TimeSpan(0))},
                        { "Authors", new [] { "Leonard G. Lobel", "Eric D. Boyd" }},
                        { "OtherCategories", new [] { CategoryOt.Book, CategoryOt.Magazine}}
                    }
                }
            };

            [EnableQuery]
            public IQueryable<BookOt> Get()
            {
                return _books.AsQueryable();
            }

            public IActionResult Get([FromODataUri]string key)
            {
                BookOt book = _books.FirstOrDefault(e => e.ISBN == key);
                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }

            /*
                https://localhost:44358/service/books
            
                {
                  "ISBN":"978-0-7356-8383-9","Title":"Programming Microsoft ASP.NET MVC","Press":{
                  "Name":"Microsoft Press","Category":"Book"
                   }, "Price": 49.99, "Published":"2014-02-15T00:00:00Z"
                }
            */
            public IActionResult Post(BookOt book)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // For this sample, we aren't enforcing unique keys.
                _books.Add(book);
                return Created(book);
            }
        }
    }
}
 