using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Tutorials_RazorPagesMovieMvc.Controllers
{
    public class HelloWorldController : Controller
    {
        //public string Index()
        //{
        //    return "This is my default action...";
        //}

        // GET: /HelloWorld/Welcome/ 
        //public string Welcome()
        //{
        //    return "This is the Welcome action method...";
        //}

        // HelloWorld/Welcome?name=Fero&numtimes=4
        //public string Welcome(string name, int numTimes = 1)
        //{
        //    return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        //}
        
        /// <summary>
        /// http://localhost:60587/helloworld
        /// </summary>
        /// <returns></returns
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// HelloWorld/Welcome/3?name=Fero
        /// </summary>
        //public string Welcome(string name, int ID = 1)
        //{
        //    return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        //}

        // HelloWorld/Welcome?name=Fero&numtimes=4
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}