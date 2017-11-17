using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Fundamentals_StaticFiles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory()) // 1. Serving static files
                .UseIISIntegration()
                .UseStartup<StatupUseFileServer>()
                .Build();

            host.Run();
        }
    }
}
