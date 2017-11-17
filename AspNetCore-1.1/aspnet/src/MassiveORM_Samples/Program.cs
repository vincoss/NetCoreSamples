using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MassiveORM_Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var table = new DynamicModel(configuration["ConnectionStrings:Northwind"]);

            var customers = table.Query("SELECT * FROM Customers");

            foreach(var c in customers)
            {
                Console.WriteLine("Id: {0}, CompanyName: {1}, ContactName: {2}", c.CustomerID, c.CompanyName, c.ContactName);
            }

            Console.Read();
        }
    }
}
