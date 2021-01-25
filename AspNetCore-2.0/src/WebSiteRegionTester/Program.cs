using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSiteRegionTester
{
    public class Program
    {
        private static string _environment;

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); 
                    webBuilder.UseConfiguration(GetConfiguration(args));
                })
                .ConfigureHostConfiguration((cfg) =>
                {
                     cfg.AddConfiguration(GetConfiguration(args));
                })
                .ConfigureAppConfiguration((hostingContext, cfg) =>
                {
                    cfg.AddConfiguration(GetConfiguration(args));
                });

        private static IConfiguration GetConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("hostsettings.json", optional: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            var config = builder.Build();
            _environment = config.GetValue<string>("Environment", null);
            var env = TrimEnvironment(_environment);

            if (string.IsNullOrWhiteSpace(env) == false)
            {
                builder.AddJsonFile($"appsettings.{env}.json".Replace("..", "."), optional: false, reloadOnChange: true);
            }

            return builder.Build();
        }

        private static string TrimEnvironment(string value)
        {
            if (string.Equals("Production", value, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            return value;
        }
    }
}
