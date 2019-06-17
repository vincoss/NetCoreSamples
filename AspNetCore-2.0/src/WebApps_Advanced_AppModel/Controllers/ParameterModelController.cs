using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_Advanced_AppModel.Conventions;

namespace WebApps_Advanced_AppModel.Controllers
{
    public class ParameterModelController : Controller
    {
        // Will bind:  /ParameterModel/GetById/123
        // WON'T bind: /ParameterModel/GetById?id=123
        public string GetById([MustBeInRouteParameterModelConvention]int id)
        {
            return $"Bound to id: {id}";
        }
    }
}
