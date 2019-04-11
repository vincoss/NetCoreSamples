using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Logging;

namespace Fundamentals_Configuration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureAppConfiguration_Sample(args);
        }

        public static void Default_Sample(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                                 .UseStartup<Startup>();

            var host = builder.Build();
            host.Run();
        }

        /*
            A typical sequence of configuration providers is:

            1.Files (appsettings.json, appsettings.{Environment}.json, where {Environment} is the app's current hosting environment)
            2.Azure Key Vault
            3.User secrets (Secret Manager) (in the Development environment only)
            4.Environment variables
            5.Command-line arguments
        */

        #region ConfigureAppConfiguration

        public static Dictionary<string, string> arrayDict = new Dictionary<string, string>
        {
            {"array:entries:0", "value0"},
            {"array:entries:1", "value1"},
            {"array:entries:2", "value2"},
            {"array:entries:4", "value4"},
            {"array:entries:5", "value5"}
        };

        public static void ConfigureAppConfiguration_Sample(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddInMemoryCollection(arrayDict);
                config.AddJsonFile("json_array.json", optional: false, reloadOnChange: false);
                //config.AddJsonFile("starship.json", optional: false, reloadOnChange: false);
                //config.AddXmlFile("tvshow.xml", optional: false, reloadOnChange: false);
                //config.AddEFConfiguration(options => options.UseInMemoryDatabase("InMemoryDb"));
                config.AddCommandLine(args);
            })
            .UseStartup<Startup>()
            .Configure(app =>
            {
                var sb = new StringBuilder();
                var config = (IConfiguration)app.ApplicationServices.GetService(typeof(IConfiguration));

                foreach (var child in config.GetChildren())
                {
                    sb.AppendLine(string.Format($"*Path: {child.Path}"));
                    sb.AppendLine(string.Format($"  Key:    {child.Key}"));
                    sb.AppendLine(string.Format($"  Value:  {child.Value}"));
                }

                var message = string.Format($@"Hello World! {Environment.NewLine}, {GetDictionaryToString(app.Properties)}, 
                    {Environment.NewLine}, {sb.ToString()}");
                app.Run(context => context.Response.WriteAsync(message));
            });

            var host = builder.Build();

            host.Run();
        }

        #endregion

        #region Command-line Configuration Provider

        private static void CommandLineConfigurationProvider_Sample(string[] args)
        {
            // Note: Best practive is to call the 'AddCommandLine' last.

            var builder =  WebHost.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((hostingContext, config) =>
           {
                // Call other providers here and call AddCommandLine last.
                config.AddCommandLine(args);
           })
           .UseStartup<Startup>();

            var host = builder.Build();

            host.Run();
        }

        private static void CommandLineConfigurationProvider_WebHostBuilder_Sample(string[] args)
        {
            var config = new ConfigurationBuilder()
            // Call additional providers here as needed.
            // Call AddCommandLine last to allow arguments to override other configuration.
            .AddCommandLine(args)
            .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>();

            host.Start();
        }

        #endregion

        private static string GetDictionaryToString(IDictionary<string, object> properties)
        {
            var sb = new StringBuilder();
            foreach(var pair in properties)
            {
                if(sb.Length > 0)
                {
                    sb.Append(Environment.NewLine);
                }
                sb.Append(string.Format($"{pair.Key}: {pair.Value}"));
            }
            return sb.ToString();
        }
    }
}
