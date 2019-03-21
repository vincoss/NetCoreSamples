using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData_Samples.Conventions
{
    public class NavigationIndexRoutingConvention : EntitySetRoutingConvention
    {
        // TOOD: see the decompiled code and implement sample, then should be registered in Startup.cs

        //public override string SelectAction(ODataPath odataPath, SelectControllerResult context, ILookup<string, IEnumerable<ControllerActionDescriptor> actionDescriptors)
        //{
        //    if (context.Request.Method == HttpMethod.Get &&
        //        odataPath.PathTemplate == "~/entityset/key/navigation/key")
        //    {
        //        NavigationPathSegment navigationSegment = odataPath.Segments[2] as NavigationPathSegment;
        //        IEdmNavigationProperty navigationProperty = navigationSegment.NavigationProperty.Partner;
        //        IEdmEntityType declaringType = navigationProperty.DeclaringType as IEdmEntityType;

        //        string actionName = "Get" + declaringType.Name;
        //        if (actionMap.Contains(actionName))
        //        {
        //            // Add keys to route data, so they will bind to action parameters.
        //            KeyValuePathSegment keyValueSegment = odataPath.Segments[1] as KeyValuePathSegment;
        //            context.RouteData.Values[ODataRouteConstants.Key] = keyValueSegment.Value;

        //            KeyValuePathSegment relatedKeySegment = odataPath.Segments[3] as KeyValuePathSegment;
        //            context.RouteData.Values[ODataRouteConstants.RelatedKey] = relatedKeySegment.Value;

        //            return actionName;
        //        }
        //    }
        //    // Not a match.
        //    return null;
        //}
    }
}



