using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Fundamentals_Hosting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Example comman line arguments
            // dotnet run --urls "http://*:5000" (Here -urls parameters will use UseUrls

            SampleConfig(args);
        }

        public static void SampleConfigRunAndStart(string[] args)
        {
            var host = new WebHostBuilder()
                   .UseSetting("ApplicationName", "SampleApplication")
                   .UseContentRoot(@"C:\TestSite")
                   .UseKestrel()

                   .Build();

            // Here to start the host application use Run or Start

            host.Run(); // Will block main thread

            // host.Start(); // Will not block main thread
            // Console.Read(); // To keep it open
        }

        public static void SampleConfig(string[] args)
        {
            var config = new ConfigurationBuilder()
              .AddCommandLine(args)
              .AddJsonFile("hosting.json", optional: true)
              .Build();

            // Can configure without Startup class, just use extension methods

            var host = new WebHostBuilder()
                    .UseSetting("ApplicationName", "SampleApplication")
                    .UseContentRoot(@"C:\TestSite")
                    .CaptureStartupErrors(true) // Will try to capture errors and start server then display those
                    .UseSetting("detailedErrors", "true") // Default is false, to show detailed errors set to true
                    .UseEnvironment("Development")    // Default is Production, see launchSettings.json for visual studio settings
                    .UseUrls("http://*:5000;http://localhost:5001;https://hostname:5002") // for * use any IP address to serve the request
                    .UseStartup("StartupAssemblyName") // Specify assembly name to search for Startup class
                    .UseWebRoot("public")   // if not specified default is contentRootPaht\wwwroot
                    .UseConfiguration(config)
                    .UseKestrel()
                    .Configure(app =>
                    {
                        app.Run(async (context) =>
                        {
                            context.ShowConnectionInfoDetails();

                            await context.Response.WriteAsync("Hola!");

                            foreach(var pair in app.Properties)
                            {
                                await context.Response.WriteAsync(string.Format("{0}Key: {1}, Value: {2}", Environment.NewLine, pair.Key, pair.Value));
                            }

                        });
                    })
                    
                    .Build();

           

            host.Run();
        }

        public static void OrderingImportanc(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = new WebHostBuilder()
                .UseUrls("http://*:1000") // default URL
                .UseConfiguration(config) // override from command line
                .UseKestrel()
                .Build();
        }
    }


    public static class CoreHelpers
    {
        public static void ShowConnectionInfoDetails(this HttpContext context)
        {
            var builder = new StringBuilder();
            builder.AppendFormat(context.Connection.ClientCertificate.ToString());
            builder.AppendFormat(context.Connection.LocalIpAddress.ToString());
            
            context.Response.WriteAsync(builder.ToString());
        }
    }
}
