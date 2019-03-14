using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_OData_Sample.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace Data_OData_Sample.Controllers
{
    public class AddressController : ODataController
    {
        private AdventureWorks2014Context _db;

        public AddressController(AdventureWorks2014Context AdventureWorks2016Context)
        {
            _db = AdventureWorks2016Context;
        }

        [EnableQuery(PageSize = 20)]
        public IActionResult Get()
        {
            return Ok(_db.Address.AsQueryable());
        }

        [EnableQuery(PageSize = 20)]
        public IActionResult Get([FromODataUri] int key)
        {
            return Ok(_db.Address.Find(key));
        }
    }
}