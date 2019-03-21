using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using OData_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData_Samples.Controllers
{
    /// <summary>
    /// service/Umbrella/Revenue
    /// </summary>
    public class UmbrellaController : ODataController
    {
        public static Company Umbrella;

        static UmbrellaController()
        {
            InitData();
        }

        private static void InitData()
        {
            Umbrella = new Company()
            {
                ID = 1,
                Name = "Umbrella",
                Revenue = 1000,
                Category = CompanyCategory.Communication,
                Employees = new List<Employee>()
            };
        }

        [ODataRoute("Umbrella/Revenue")]
        public IActionResult GetCompanyRevenue()
        {
            return Ok(Umbrella.Revenue);
        }

        #region Method signatures for the singleton controller

        // Method signatures for every action definition in the singleton controller

        // Get Singleton 
        // ~/singleton 
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        public IActionResult GetUmbrella()
        {
            throw new NotImplementedException();
        }

        // Get Singleton 
        // ~/singleton/cast 
        public IActionResult GetFromSubCompany()
        {
            throw new NotImplementedException();
        }

        public IActionResult GetUmbrellaFromSubCompany()
        {
            throw new NotImplementedException();
        }

        // Get Singleton Property 
        // ~/singleton/property  
        public IActionResult GetName()
        {
            throw new NotImplementedException();
        }

        public IActionResult GetNameFromCompany()
        {
            throw new NotImplementedException();
        }
        
        // Get Singleton Navigation Property 
        // ~/singleton/navigation  
        public IActionResult GetEmployees()
        {
            throw new NotImplementedException();
        }

        public IActionResult GetEmployeesFromCompany()
        {
            throw new NotImplementedException();
        }

        // Update singleton by PUT 
        // PUT ~/singleton 
        public IActionResult Put(Company newCompany)
        {
            throw new NotImplementedException();
        }

        public IActionResult PutUmbrella(Company newCompany)
        {
            throw new NotImplementedException();
        }

        // Update singleton by Patch 
        // PATCH ~/singleton 
        public IActionResult Patch(Delta<Company> item)
        {
            throw new NotImplementedException();
        }

        public IActionResult PatchUmbrella(Delta<Company> item)
        {
            throw new NotImplementedException();
        }

        // Add navigation link to singleton 
        // POST ~/singleton/navigation/$ref 
        public IActionResult CreateRef(string navigationProperty, [FromBody] Uri link)
        {
            throw new NotImplementedException();
        }

        // Delete navigation link from singleton 
        // DELETE ~/singleton/navigation/$ref?$id=~/relatedKey 
        public IActionResult DeleteRef(string relatedKey, string navigationProperty)
        {
            throw new NotImplementedException();
        }

        // Add a new entity to singleton navigation property 
        // POST ~/singleton/navigation 
        public IActionResult PostToEmployees([FromBody] Employee employee)
        {
            throw new NotImplementedException();
        }

        // Call function bounded to singleton 
        // GET ~/singleton/function() 
        public IActionResult GetEmployeesCount()
        {
            throw new NotImplementedException();
        } 

        #endregion
    }
}
