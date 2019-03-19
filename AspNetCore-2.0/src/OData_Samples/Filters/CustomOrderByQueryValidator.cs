using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.OData;
using Microsoft.OData.UriParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData_Samples.Filters
{
    public class CustomOrderByQueryValidator : OrderByQueryValidator
    {
        public CustomOrderByQueryValidator(DefaultQuerySettings settings) : base(settings) { }

        // Disallow the 'desc' parameter for $orderby option.
        public override void Validate(OrderByQueryOption orderByOption, ODataValidationSettings validationSettings)
        {
            if (orderByOption.OrderByNodes.Any(node => node.Direction == OrderByDirection.Descending))
            {
                throw new ODataException("The 'desc' option is not supported.");
            }
            base.Validate(orderByOption, validationSettings);
        }
    }
}
