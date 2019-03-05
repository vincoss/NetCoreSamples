using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fundamentals_DependencyInjection.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fundamentals_DependencyInjection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AutofacSample(args);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static void DefaultSample(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            host.Run();
        }

        #region Call services from main

        public static void CallServicesFromMain(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var serviceContext = services.GetRequiredService<MyScopedService>();
                    // Use the context here
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred.");
                }
            }

            host.Run();
        }

        #endregion

        #region Autofac sample

        public static void AutofacSample(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                              .UseStartup<AutofacStartup>()
                             .Build();

            host.Run();
        }

        #endregion
    }
}
