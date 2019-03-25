using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using OData_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OData_Samples.Controllers
{
    /*
        The service document lists entity sets, functions, and singletons that can be retrieved.
        https://localhost:44358/service
        The metadata document describes the types, sets, function and action understand by OData service.
        https://localhost:44358/service/$metadata

        The URIs for the dynamic resources
        https://localhost:44358/service/foos
        https://localhost:44358/service/foos(103)

        https://localhost:44358/service/FooBarRecs?$expand=FooRec,BarRec
    */

    public class FoosController : ODataController
    {
        private const int _num = 10;
        public static IList<Foo> Foos = Enumerable.Range(0, _num).Select(i =>
               new Foo
               {
                   FooId = 100 + i,
                   FooName = "Foo #" + (100 + i)
               }).ToList();
        
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(Foos);
        }

        public IActionResult Get([FromODataUri]int key)
        {
            Foo book = Foos.FirstOrDefault(e => e.FooId == key);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
    }

    public class BarsController : ODataController
    {
        private const int _num = 10;
        public static IList<Bar> Bars = Enumerable.Range(0, _num).Select(i =>
               new Bar
               {
                   BarId = 1000 + i,
                   BarName = "Bar #" + (1000 + i)
               }).ToList();

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(Bars);
        }
    }

    public class FooBarRecsController : ODataController
    {
        private const int _num = 10;
        public static IList<FooBarRec> FooBarRecs = Enumerable.Range(0, _num).Select(i =>
                new FooBarRec
                {
                    Id = i,
                    Name = "ForBarRec #" + i
                }).ToList();

        static FooBarRecsController()
        {
            for (int i = 0; i < _num; i++)
            {
                FooBarRecs[i].FooRec = FoosController.Foos[i];
                FooBarRecs[i].BarRec = BarsController.Bars[i];
            }
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(FooBarRecs);
        }
    }
}
