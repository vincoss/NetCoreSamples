﻿using Data_OData_Sample.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_OData_Sample.Controllers
{
    [ODataRoutePrefix("Person")]
    public class PersonController : ODataController
    {
        private readonly AdventureWorks2014Context _db;

        public PersonController(AdventureWorks2014Context AdventureWorks2016Context)
        {
            _db = AdventureWorks2016Context;
        }

        [ODataRoute]
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IActionResult Get()
        {
            return Ok(_db.Person.AsQueryable());
        }

        [ODataRoute("({key})")]
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IActionResult Get([FromODataUri] int key)
        {
            return Ok(_db.Person.Find(key));
        }

        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
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
