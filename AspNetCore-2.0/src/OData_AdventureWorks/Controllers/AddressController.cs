using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OData_AdventureWorks.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OData_AdventureWorks.Data;

namespace OData_AdventureWorks.Controllers
{
    public class AddressController : ODataController
    {
        private AdventureWorks2014Context _db;

        public AddressController(AdventureWorks2014Context context)
        {
            _db = context;
        }

        [EnableQuery(PageSize = Constants.PageSize)]
        public IActionResult Get()
        {
            return Ok(_db.Address.AsQueryable());
        }

        [EnableQuery(PageSize = Constants.PageSize)]
        public IActionResult Get([FromODataUri] int key)
        {
            return Ok(_db.Address.Find(key));
        }
    }
}