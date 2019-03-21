using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using OData_Samples.Data;
using OData_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData_Samples.Controllers
{
    /// <summary>
    /// service
    /// service/&metadata
    /// 
    /// Sorting
    /// 
    /// $orderby
    /// service/keywords?$orderby=Name
    /// service/keywords?$orderby=Weight desc
    /// service/keywords?$orderby=Weight,Name desc
    /// 
    /// Paging
    /// 
    /// $top & $skip
    /// service/keywords?$top=2&$skip=1
    /// 
    /// List number of items
    /// service/keywords?$count=true
    /// 
    /// Filtering
    /// 
    /// service/keywords?$filter=Name eq 'as'
    /// service/keywords?$filter=Weight lt 1.3
    /// service/keywords?$filter=Weight ge 1.1 and Weight le 1.4
    /// service/keywords?$filter=contains(Name, 'oo')
    /// service/keywords?$filter=year(ReleaseDate) gt 2005
    /// 
    /// </summary>
    public class KeywordsController : ODataController
    {
        [EnableQuery(PageSize = 10)] // Enable OData query options for the action, page for large size.
        public IActionResult Get()
        {
            return Ok(SampleData.Keywords);
        }

        // Non OData format use this method
        public PageResult<Keyword> Get(ODataQueryOptions<Keyword> options)
        {
            ODataQuerySettings settings = new ODataQuerySettings()
            {
                PageSize = 5
            };

            IQueryable results = options.ApplyTo(SampleData.Keywords.AsQueryable(), settings);

            return new PageResult<Keyword>(results as IEnumerable<Keyword>, Request.GetNextPageLink(10), 1000); // TODO: this is wrong since the could not figure it in .NET Core
        }
    }

    public class VariablesController : ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(SampleData.Variables);
        }
    }

}
