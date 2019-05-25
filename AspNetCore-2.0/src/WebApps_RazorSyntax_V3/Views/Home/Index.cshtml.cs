using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApps_RazorSyntax_V3.Models;

namespace WebApps_RazorSyntax_V3.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
            People = new[] { new Person("Ferdinand", 123) };
        }

        public void OnGet()
        {

        }

        public int Value { get; } = 3;

        public string UserName { get; } = "Ferdinand";

        public Person[] People { get; set; }

        public Task<string> DoSomething(string a, string b)
        {
            return Task.FromResult(string.Format($"{a} - {b}"));
        }

        public T GenericMethod<T>(T value)
        {
            return value;
        }
    }
}
