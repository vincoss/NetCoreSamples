using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using OData_Samples.Filters;
using OData_Samples.Models;

namespace OData_Samples.Controllers
{
    /// <summary>
    /// NOTE: No runnable only for reference.
    /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-security-guidance
    /// </summary>
    public class SecurityGuidanceController : ODataController
    {
        [EnableQuery(PageSize = 10, AllowedQueryOptions = AllowedQueryOptions.Skip | AllowedQueryOptions.Top, AllowedOrderByProperties="Id,Name")] // Server side pagging
        public IActionResult Get()
        {
            return Ok();
        }

        [EnableQuery(MaxNodeCount = 20)] // Set the maximum node count.
        public IActionResult GetA()
        {
            return Ok();
        }

        [EnableQuery(AllowedFunctions = AllowedFunctions.AllFunctions & ~AllowedFunctions.All & ~AllowedFunctions.Any)] // Disable any() and all() functions.
        public IActionResult GetB()
        {
            return Ok();
        }

        [EnableQuery(AllowedFunctions = AllowedFunctions.AllFunctions & ~AllowedFunctions.AllStringFunctions)] // Disable string functions.
        public IActionResult GetC()
        {
            return Ok();
        }

        // Allow the "eq" logical function but no other logical functions:
        [EnableQuery(AllowedLogicalOperators = AllowedLogicalOperators.Equal)]
        public IActionResult GetD()
        {
            return Ok();
        }

        // Do not allow any arithmetic operators:
        [EnableQuery(AllowedArithmeticOperators = AllowedArithmeticOperators.None)]
        public IActionResult GetE()
        {
            return Ok();
        }

        // Invoking Query Options Directly
        public IQueryable<Product> Get(ODataQueryOptions opts)
        {
            // example products form EF
            var products = new HashSet<Product>();

            var settings = new ODataValidationSettings()
            {
                // Initialize settings as needed.
                AllowedFunctions = AllowedFunctions.AllMathFunctions
            };

            opts.Validate(settings);

            IQueryable results = opts.ApplyTo(products.AsQueryable());
            return results as IQueryable<Product>;
        }

        public IQueryable<Product> GetF(ODataQueryOptions opts)
        {
            var products = new HashSet<Product>();
            if (opts.OrderBy != null)
            {
                opts.OrderBy.Validator = new CustomOrderByQueryValidator(opts.Context.DefaultQuerySettings);
            }

            var settings = new ODataValidationSettings()
            {
                // Initialize settings as needed.
                AllowedFunctions = AllowedFunctions.AllMathFunctions
            };

            // Validate
            opts.Validate(settings);

            IQueryable results = opts.ApplyTo(products.AsQueryable());
            return results as IQueryable<Product>;
        }

        [CustomQueryable]
        public IQueryable<Product> GetG()
        {
            var products = new HashSet<Product>();
            return products.AsQueryable();
        }
    }
}