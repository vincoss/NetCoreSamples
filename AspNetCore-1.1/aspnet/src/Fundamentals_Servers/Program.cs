using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace Fundamentals_Servers
{
    public class Program
    {
        /// <summary>
        /// http://localhost:5000/
        /// </summary>
        public static int Main(string[] args)
        {
            var buildder = new ConfigurationBuilder();
            buildder.AddInMemoryCollection()
            .AddEnvironmentVariables();

            var config = buildder.Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();

            host.Start();
            //host.Run();

            Console.WriteLine("Started the server..");
            Console.WriteLine("Press any key to stop the server");
            Console.ReadLine();

            return 0;
        }
    }
}
