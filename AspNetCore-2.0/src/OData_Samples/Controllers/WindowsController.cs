using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData_Samples.Controllers
{
    public class WindowsController : ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(null);
        }
    }
}
