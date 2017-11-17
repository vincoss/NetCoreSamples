using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GettingStarted_WebApiSample.Services;
using GettingStarted_WebApiSample.Models;


namespace GettingStarted_WebApiSample.Controllers
{
    [Produces("application/json")]
    [Route("api/Toto")]
    public class TotoController : Controller
    {
        private readonly IDatabaseService _context;

        public TotoController(IDatabaseService context)
        {
            _context = context;

            if (_context.TotoItems.Count() == 0)
            {
                _context.AddTotoAsync(new TotoItem { Name = "Shovel" } );
                _context.AddTotoAsync(new TotoItem { Name = "Mattock" });
            }
        }

        //  http://localhost:50947/api/toto
        [HttpGet]
        public IEnumerable<TotoItem> GetAll()
        {
            return _context.TotoItems.ToList();
        }

        // http://localhost:50947/api/toto/1
        // http://localhost:50947/api/toto/2
        // http://localhost:50947/api/toto/3 -- NOT found 404 if item does not exists
        [HttpGet("{id}", Name = "GetToto")]
        public IActionResult GetById(long id)
        {
            var item = _context.TotoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        /* 
         
            Postman details

            URL     http://localhost:50947/api/toto/
            TYPE    POST
            BODY    RAW
            TYPE    JSON

            {
	            "name": "Rake",
	            "isComplete": false
            }

        */
        [HttpPost]
        public IActionResult Create([FromBody] TotoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.AddTotoAsync(item);

            return CreatedAtRoute("GetToto", new { id = item.Id }, item);
        }

        /* 

           Postman details

           URL     http://localhost:50947/api/toto/1
           TYPE    PUT
           BODY    RAW
           TYPE    JSON

           {
               "id": "1",
               "name": "Rake",
               "isComplete": false
           }

       */
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TotoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = _context.TotoItems.SingleOrDefault(x => x.Id == item.Id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.UpdateTototAsync(todo);

            return new NoContentResult();
        }

        /* 

          Postman details

          URL     http://localhost:50947/api/toto/2
          TYPE    DELETE
          BODY    RAW
          TYPE    JSON

        */
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TotoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.RemoveAsync(todo);

            return new NoContentResult();
        }
    }
}