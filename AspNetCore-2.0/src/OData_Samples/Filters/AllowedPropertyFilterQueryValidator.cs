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
    public class AllowedPropertyFilterQueryValidator : FilterQueryValidator
    {
        public AllowedPropertyFilterQueryValidator(DefaultQuerySettings settings) : base(settings)
        {
        }

        private static readonly string[] allowedProperties = { "ReleaseYear", "Name" };

        public override void ValidateSingleValuePropertyAccessNode(SingleValuePropertyAccessNode propertyAccessNode, ODataValidationSettings settings)
        {
            string propertyName = null;
            if (propertyAccessNode != null)
            {
                propertyName = propertyAccessNode.Property.Name;
            }
            if (propertyName != null && !allowedProperties.Contains(propertyName))
            {
                throw new ODataException(string.Format("Filter on {0} not allowed", propertyName));
            }
            base.ValidateSingleValuePropertyAccessNode(propertyAccessNode, settings);
        }
    }
}
