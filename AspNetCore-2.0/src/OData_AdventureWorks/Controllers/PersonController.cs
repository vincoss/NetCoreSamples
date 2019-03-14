using OData_AdventureWorks.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData_AdventureWorks.Controllers
{
    [ODataRoutePrefix("Person")]
    public class PersonController : ODataController
    {
        private readonly AdventureWorks2014Context _db;

        public PersonController(AdventureWorks2014Context context)
        {
            _db = context;
        }

        [ODataRoute]
        [EnableQuery(PageSize = Constants.PageSize, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IActionResult Get()
        {
            return Ok(_db.Person.AsQueryable());
        }

        [ODataRoute("({key})")]
        [EnableQuery(PageSize = Constants.PageSize, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IActionResult Get([FromODataUri] int key)
        {
            return Ok(_db.Person.Find(key));
        }

        [EnableQuery(PageSize = Constants.PageSize, AllowedQueryOptions = AllowedQueryOptions.All)]
        [ODataRoute("Default.MyFirstFunction")]
        [HttpGet]
        public IActionResult MyFirstFunction()
        {
            return Ok(_db.Person.Where(t => t.FirstName.StartsWith("K")));
        }



        //[HttpGet]
        //[EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        //[ODataRoute("Default.PersonSearchPerPhoneType(PhoneNumberTypeEnum={phoneNumberTypeEnum})")]
        //public IActionResult PersonSearchPerPhoneType([FromODataUri] PhoneNumberTypeEnum phoneNumberTypeEnum)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    return Ok("");
        //}
    }
}
