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
using Microsoft.EntityFrameworkCore;
using Fundamentals_Configuration.Models;
using Fundamentals_Configuration.Extensions;

namespace Fundamentals_Configuration
{
    /// <summary>
    /// dotnet run quote1="This is from args"
    /// dotnet run --quote1="This is from args"
    /// dotnet run /quote1="This is from args"
    /// </summary>
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
                config.AddJsonFile("json_array2.json", optional: false, reloadOnChange: false);
                config.AddXmlFile("appsettings.xml", optional: false, reloadOnChange: false);
                config.AddEFConfiguration(options => options.UseInMemoryDatabase("InMemoryDb"));
                config.AddCommandLine(args);
            })
            .UseStartup<Startup>()
            .Configure(app =>
            {
                var sb = new StringBuilder();
                var config = (IConfiguration)app.ApplicationServices.GetService(typeof(IConfiguration));
                var items = config.AsEnumerable();
               
                foreach (var child in config.AsEnumerable())
                {
                    sb.AppendLine(string.Format($"*Path: {child.GetType().Name}"));
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

        public static readonly Dictionary<string, string> _switchMappings = new Dictionary<string, string>
        {
            { "-CLKey1", "CommandLineKey1" },
            { "-CLKey2", "CommandLineKey2" }
        };

        // dotnet run -CLKey1=value1 -CLKey2=value2
        private static void CommandLineConfigurationProvider_SwitchMappings_Sample(string[] args)
        {
            var config = new ConfigurationBuilder()
            // Call additional providers here as needed.
            // Call AddCommandLine last to allow arguments to override other configuration.
            .AddCommandLine(args, _switchMappings)
            .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>();

            host.Start();
        }

        #endregion

        // Environment Variables Configuration Provider

        public static void EnvironmentVariables_Sample(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                                 .ConfigureAppConfiguration((hostingContext, config) =>
                                 {
                                     // Call additional providers here as needed.
                                     // Call AddEnvironmentVariables last if you need to allow environment
                                     // variables to override values from other providers.
                                     config.AddEnvironmentVariables(prefix: "PREFIX_");
                                 })
                                 .UseStartup<Startup>();

            var host = builder.Build();
            host.Run();

            /*
            // Direct example
                
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>();
            */
        }

        // File Configuration Provider

        public static void FileConfigurationProvider_Sample(string[] args)
        {
            // INI Configuration Provider
            // JSON
            // XML

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddIniFile("appsettings.ini", false, true)
                .AddJsonFile("appsettings.json", false)
                .AddXmlFile("appsettings.xml", false)
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>();
        }

        // Key-per-file Configuration Provider

        public static void KeyPerFileConfigurationProvider_Sample(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "path/to/files");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddKeyPerFile(directoryPath: path, optional: true)
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>();
        }

        // Memory Configuration Provider

        public static readonly Dictionary<string, string> _memoryDict = new Dictionary<string, string>
        {
                {"MemoryCollectionKey1", "value1"},
                {"MemoryCollectionKey2", "value2"}
        };

        public static void MemoryConfigurationProvider_Sample(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddInMemoryCollection(_memoryDict)
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>();
        }

        #region GetValue, GetSection, GetChildren, and Exists

        public static void GetValue_Sample(string[] args)
        {
            var config = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddIniFile("appsettings.ini", false)
                  .Build();

            var host = new WebHostBuilder()
                    .UseConfiguration(config)
                    .UseKestrel()
                    .UseStartup<Startup>()
                    .Configure(app =>
                    {
                        var sb = new StringBuilder();
                        var appConfig = (IConfiguration)app.ApplicationServices.GetService(typeof(IConfiguration));

                        var value = appConfig.GetValue<int>("TheKey", 99);
                        var configSection = appConfig.GetSection("section0");
                        var configSection2 = appConfig.GetSection("section2:subsection0");

                        sb.AppendLine(string.Format($"TheKey: {value}"));
                        sb.AppendLine(string.Format($"section1: {configSection.Value}"));
                        sb.AppendLine(string.Format($"section2:subsection: {configSection2.Value}"));

                        // Children
                        var children = configSection.GetChildren();

                        // Exists
                        var sectionExists = appConfig.GetSection("section2:subsection2").Exists();

                        var message = string.Format($@"Hello World! {Environment.NewLine}, {sb.ToString()}");
                        app.Run(context => context.Response.WriteAsync(message));
                    });

            host.Build().Run();

        }

        #endregion

        #region Bind to a class, Bind to an object graph, Bind an array to a class

        public static void BindToAClass_Sample(string[] args)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("starship.json", false)
                 .AddXmlFile("tvshow.xml")
                 .AddInMemoryCollection(arrayDict)
                 .AddJsonFile("missing_value.json", false)
                 .Build();

            var host = new WebHostBuilder()
                    .UseConfiguration(config)
                    .UseKestrel()
                    .UseStartup<Startup>()
                    .Configure(app =>
                    {
                        var sb = new StringBuilder();
                        var appConfig = (IConfiguration)app.ApplicationServices.GetService(typeof(IConfiguration));

                        var starship = new Starship();
                        appConfig.GetSection("starship").Bind(starship);

                        var tvShow = new TvShow();
                        appConfig.GetSection("tvshow").Bind(tvShow);

                        var tvShow2 = appConfig.GetSection("tvshow").Get<TvShow>();

                        var arrayExample = new ArrayExample();
                        appConfig.GetSection("array").Bind(arrayExample);
                        var arrayExample2 = appConfig.GetSection("array").Get<ArrayExample>();

                        var message = string.Format($@"Hello World! {Environment.NewLine}, {sb.ToString()}");
                        app.Run(context => context.Response.WriteAsync(message));
                    });

            host.Build().Run();
        }

        #endregion

        #region Custom configuration provider


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
