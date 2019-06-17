using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApps_Advanced_AppModel.Conventions;


namespace WebApps_Advanced_AppModel.Controllers
{
    [ControllerDescription("Controller Description")]
    public class DescriptionAttributesController : Controller
    {
        public string Index()
        {
            return "Description: " + ControllerContext.ActionDescriptor.Properties["description"];
        }

        [ActionDescription("Action Description")]
        public string UseActionDescriptionAttribute()
        {
            return "Description: " + ControllerContext.ActionDescriptor.Properties["description"] + nameof(UseActionDescriptionAttribute);
        }
    }
}