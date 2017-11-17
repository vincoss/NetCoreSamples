using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace Fundamentals_Logging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebSiteWithLoggingAndConfigFile();

            Console.WriteLine("Done...");
            Console.Read();
        }

        public static void WebSiteWithLoggingAndConfigFile()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
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
