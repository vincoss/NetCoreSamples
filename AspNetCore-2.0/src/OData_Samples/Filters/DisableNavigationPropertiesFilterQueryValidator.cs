using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OData_Samples.Filters
{
    /// <summary>
    /// Validator to prevent filtering on navigation properties.
    /// </summary>
    public class DisableNavigationPropertiesFilterQueryValidator : FilterQueryValidator
    {
        public DisableNavigationPropertiesFilterQueryValidator(DefaultQuerySettings settings) : base(settings)
        {

        }

        public override void ValidateNavigationPropertyNode(QueryNode sourceNode, IEdmNavigationProperty navigationProperty, ODataValidationSettings settings)
        {
            throw new ODataException("No navigation properties.");
        }
    }
}
