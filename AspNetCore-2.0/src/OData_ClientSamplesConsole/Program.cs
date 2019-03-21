using OData_ClientSamplesConsole.Models;
using Simple.OData.Client;
using System;
using System.Collections.Generic;

namespace OData_ClientSamplesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductSamples();

            Console.WriteLine("Done...");
            Console.Read();
        }

        public async static void ProductSamples()
        {
            var serviceUrl = "https://localhost:44358/service";
            var client = new ODataClient(serviceUrl);

            // Basic API
            Console.WriteLine("Basic API");

            var prouducts = await client.FindEntriesAsync("Products?$filter=Name eq 'Chai'");
            foreach (var product in prouducts)
            {
                Console.WriteLine(product["Name"]);
            }

            // Fluent API
            Console.WriteLine("Fluent API");

            var results = await client.For<Category>()
                    .Expand(rr => rr.Products)
                    //.Top(7).Skip(0)
                    .FindEntriesAsync();

            foreach(var result in results)
            {
                Console.WriteLine($"{result.Name}");

                foreach(var product in result.Products)
                {
                    Console.WriteLine($"    {product.Name}");
                }
            }
            
            // Fluent API (dynamic)
            Console.WriteLine("Fluent API (dynamic)");

            var x = ODataDynamic.Expression;
            IEnumerable<dynamic> products = await client
                .For(x.Products)
                .Filter(x.Name == "Chai")
                .FindEntriesAsync();

            foreach (var product in products)
            {
                Console.WriteLine(product.Name);
            }
        }
    }
}
