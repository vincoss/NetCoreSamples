using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Fundamentals_MainConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                   .UseKestrel()
                   //.UseStartup("StartupAssembly.dll") // Look for startup class in specified assembly file
                   .UseStartup<Startup>()
                   .Build();

            host.Run();
        }
    }
}
