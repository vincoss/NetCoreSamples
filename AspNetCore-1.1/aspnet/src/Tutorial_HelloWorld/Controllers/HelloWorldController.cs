using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MvcMovie_Tutorial.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: /helloworld/index
        public string Index()
        {
            return "This is my default action";
        }

        // GET: /HelloWorld/Welcome
        public string Welcome(string name, int runs = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, numTimes: {runs}");
        }

        // GET: /helloworld/WelcomeTwo/3?name=fero
        public string WelcomeTwo(string name, int id = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, numTimes: {id}");
        }

        // GET: /helloworld/indexview
        public IActionResult IndexView()
        {
            return View();
        }

        // GET: /welcomeview/3?name=hello
        public IActionResult WelcomeView(string name, int id = 1)
        {
            ViewData["Name"] = name;
            ViewData["Id"] = id;

            return View();
        }
    }
}
