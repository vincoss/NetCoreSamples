using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OData_Samples.Filters
{
    public class CustomQueryableAttribute : EnableQueryAttribute
    {
        public override void ValidateQuery(HttpRequest request, ODataQueryOptions queryOptions)
        {
            if (queryOptions.OrderBy != null)
            {
                queryOptions.OrderBy.Validator = new CustomOrderByQueryValidator(queryOptions.Context.DefaultQuerySettings);
            }
            base.ValidateQuery(request, queryOptions);
        }
    }
}
