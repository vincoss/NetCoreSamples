using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps_Mvc_Routing.Controllers
{
    public class SubscriptionManagementController : Controller
    {
        [HttpGet("[controller]/[action]")] // Matches '/subscription-management/list-all'
        public IActionResult ListAll()
        {
            throw new NotImplementedException();
        }
    }
}
