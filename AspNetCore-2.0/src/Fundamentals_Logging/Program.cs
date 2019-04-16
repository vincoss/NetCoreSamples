using Fundamentals_Logging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Fundamentals_Logging
{
    class Program
    {
        static void Main(string[] args)
        {
            AddProvidersSample(args);
        }

        // Add providers
        public static async Task AddProvidersSample(string[] args)
        {
            IHostBuilder builder = new HostBuilder()
              .ConfigureHostConfiguration(config =>
              {
                  config.Properties.Add(HostDefaults.ApplicationKey, typeof(Program).Name);
                  config.SetBasePath(Path.Combine(AppContext.BaseDirectory));
                  config.AddEnvironmentVariables();
              })
              .ConfigureAppConfiguration((hostingContext, config) =>
              {
                  hostingContext.HostingEnvironment.ApplicationName = typeof(Program).Name;
                  config.AddCommandLine(args);
              })
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddHostedService<SampleHostedService>();
              })
              .ConfigureLogging((hostingContext, logging) =>
              {
                  logging.ClearProviders();
                  logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                  logging.AddConsole();
                  logging.AddDebug();
                  logging.AddEventSourceLogger();
                  logging.AddTraceSource("Debug");
                  logging.AddEventLog();

                  // Log Scopes (enable for console apps)
                  logging.AddConsole(options => options.IncludeScopes = true);
              });

            var host = builder.Build();
            await host.RunAsync();
        }

        // Create logs in program
        public static async Task CreateLogsInProgramSample(string[] args)
        {
            IHostBuilder builder = new HostBuilder()
              .ConfigureLogging((hostingContext, logging) =>
              {
                  logging.ClearProviders();
                  logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                  logging.AddConsole();
                  logging.AddDebug();
                  logging.AddEventSourceLogger();
                  logging.AddTraceSource("Debug");

                  // Log Filtering
                  logging.AddFilter("System", LogLevel.Debug).AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Trace);

                  // Default minimum level
                  logging.SetMinimumLevel(LogLevel.Warning);

                  // Filter function
                  logging.AddFilter((provider, category, logLevel) =>
                  {
                      if (provider == "Microsoft.Extensions.Logging.Console.ConsoleLoggerProvider" &&
                          category == "TodoApiSample.Controllers.TodoController")
                      {
                          return false;
                      }
                      return true;
                  });

                  // Log Scopes (enable for console apps)
                  logging.AddConsole(options => options.IncludeScopes = true);


              });

            var host = builder.Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Program starting.");

            await host.RunAsync();
        }

        // Create logs in program
        public static async Task CreateLogsInAzureSample(string[] args)
        {
            IHostBuilder builder = new HostBuilder()
              .ConfigureServices(services =>
              {
                  services.Configure<AzureFileLoggerOptions>(options =>
                        {
                            options.FileName = "azure-diagnostics-";
                            options.FileSizeLimit = 50 * 1024;
                            options.RetainedFileCountLimit = 5;
                        }).Configure<AzureBlobLoggerOptions>(options =>
                        {
                            options.BlobName = "log.txt";
                        });
              })
              .ConfigureLogging((hostingContext, logging) =>
              {
                  logging.ClearProviders();
                  logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                  logging.AddConsole();
                  logging.AddDebug();

                  // Logging in Azure
                  logging.AddAzureWebAppDiagnostics();

              });

            var host = builder.Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Program starting.");

            await host.RunAsync();
        }
    }
}
