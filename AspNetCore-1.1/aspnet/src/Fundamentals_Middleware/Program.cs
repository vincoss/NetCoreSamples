using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_Middleware
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                     .UseKestrel()
                     .UseStartup<StartupRunMapUse>()
                     .Build();

            host.Run();
        }
    }
}
