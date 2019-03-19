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

        https://localhost:44358/service/FooBarRecs?$expand=FooRec,BarRec
    */

    public class FoosController : ODataController
    {
        public const int Num = 10;
        public static IList<Foo> foos = Enumerable.Range(0, Num).Select(i =>
               new Foo
               {
                   FooId = 100 + i,
                   FooName = "Foo #" + (100 + i)
               }).ToList();
        
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(foos);
        }
    }

    public class BarsController : ODataController
    {
        public const int Num = 10;
        public static IList<Bar> bars = Enumerable.Range(0, Num).Select(i =>
               new Bar
               {
                   BarId = 1000 + i,
                   BarName = "Bar #" + (1000 + i)
               }).ToList();

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(bars);
        }
    }

    public class FooBarRecsController : ODataController
    {
        public const int Num = 10;
        public static IList<FooBarRec> fooBarRecs = Enumerable.Range(0, Num).Select(i =>
                new FooBarRec
                {
                    Id = i,
                    Name = "ForBarRec #" + i
                }).ToList();

        static FooBarRecsController()
        {
            for (int i = 0; i < Num; i++)
            {
                fooBarRecs[i].FooRec = FoosController.foos[i];
                fooBarRecs[i].BarRec = BarsController.bars[i];
            }
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(fooBarRecs);
        }
    }
}
