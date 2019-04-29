using Fundamentals_Logging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Diagnostics;
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

        public static void CreateLoggerFactory()
        {
            ILoggerFactory factory = new LoggerFactory();
            factory.AddConsole();
            factory.AddDebug();

            var logger = factory.CreateLogger(nameof(CreateLoggerFactory));
            logger.LogInformation("LogInformation: {0}", DateTime.Now);
        }

        public static void SampleLogging()
        {
            ILoggerFactory factory = new LoggerFactory();

            factory.AddConsole();

            var logger = factory.CreateLogger(nameof(SampleLogging));
            logger.LogInformation("No endpoint found for request {path}", "Basic sample");

            logger.LogInformation("LogInformation: {0}", "basic");
            logger.LogInformation(1, "LogInformation: {0}", "with event id");
            logger.LogInformation(2, new Exception("Lol"), "LogInformation: {0}", "with exception");
        }

        public static void FilteringLogs()
        {
            ILoggerFactory factory = new LoggerFactory();

            var logger = factory.CreateLogger(nameof(FilteringLogs));
            var loggerM = factory.CreateLogger("Microsoft");

            factory.WithFilter(new FilterLoggerSettings
            {
                { "Microsoft", LogLevel.Warning },
                { "System", LogLevel.Warning },
                { "ToDoApi", LogLevel.Debug }
            }).AddConsole();

            logger.LogInformation("LogInformation: {0}", DateTime.Now);
            loggerM.LogWarning("LogWarning Microsoft: {0}", DateTime.Now);
            loggerM.LogInformation("LogInformation Microsoft: {0}", DateTime.Now);
        }

        public static void ConfiguringTraceSourceLoggingSample()
        {
            // See project.json settings
            // "Microsoft.Extensions.Logging.TraceSource": "1.0.0"

            ILoggerFactory factory = new LoggerFactory();

            var logger = factory.CreateLogger(nameof(ConfiguringTraceSourceLoggingSample));

            // add Trace Source logging
            var testSwitch = new SourceSwitch("sourceSwitch", "Logging Sample");
            testSwitch.Level = SourceLevels.Warning;
            factory.AddTraceSource(testSwitch, new TextWriterTraceListener(writer: Console.Out));

            logger.LogInformation("LogInformation"); // wont show
            logger.LogWarning("LogWarning: {0}", DateTime.Now);
        }
    }
}
