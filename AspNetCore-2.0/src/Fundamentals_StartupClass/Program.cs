using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Fundamentals_StartupClass.Startup;

namespace Fundamentals_StartupClass
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StartupWithStartupFilters(args);
        }

        #region Default config

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                       .UseStartup<DefaultStartup>();

        public static void DefaultConfig(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        #endregion

        #region Convenience methods without startup class

        public static void WithoutStartupClass(string[] args)
        {
           var host = WebHost.CreateDefaultBuilder(args)
                  .ConfigureAppConfiguration((hostingContext, config) =>
                  {
                      HostingEnvironment = hostingContext.HostingEnvironment;
                      Configuration = config.Build();
                  })
                  .ConfigureServices(services =>
                  {
                      // ...
                  })
                  .Configure(app =>
                  {
                      var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
                      var logger = loggerFactory.CreateLogger<Program>();

                      logger.LogInformation("Logged in Configure");

                      if (HostingEnvironment.IsDevelopment())
                      {
                          // ...
                      }
                      else
                      {
                          //  ...
                      }

                      var configValue = Configuration["subsection:suboption1"];

                      // ...
                      app.Run(async (context) =>
                      {
                          await context.Response.WriteAsync("Hello World!");
                      });
                  }).Build();

            host.Run();
        }

        public static IHostingEnvironment HostingEnvironment { get; set; }
        public static IConfiguration Configuration { get; set; }

        #endregion

        #region Startup with startup filters

        public static void StartupWithStartupFilters(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();
                })
                .UseStartup<DefaultStartup>()
                .Build()
                .Run();
        }

        #endregion
    }
}
