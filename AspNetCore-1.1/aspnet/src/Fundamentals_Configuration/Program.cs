using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections;
using Microsoft.AspNetCore.Hosting;

namespace Configuration_Samples
{
    // PDF 191
    public class Program
    {
        public static void Main(string[] args)
        {
            CustomProviderSample();

            Console.WriteLine("Done...");
            Console.Read();
        }

        public static void InMemorySample()
        {
            var buildder = new ConfigurationBuilder();
            buildder.AddInMemoryCollection()
            .AddEnvironmentVariables();

            var config = buildder.Build();
            config["SomeKey"] = "Hello";

            Console.WriteLine(config["SomeKey"]);
        }

        public static void AddEnvironmentVariablesSample()
        {
            var buildder = new ConfigurationBuilder()
                        .AddEnvironmentVariables();

            var config = buildder.Build();

            Console.WriteLine(config["SomeKey"]);
        }

        public static void JsonSample()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var config = builder.Build();

            Console.WriteLine(config.GetConnectionString("Northwind")); // Some extension method is build in
            Console.WriteLine(config["ConnectionStrings:Northwind"]);
            Console.WriteLine(config["Logging:LogLevel:Default"]);
        }

        public static void BuilderWithMultipleSources()
        {
            // work with with a builder using multiple calls
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var connectionStringConfig = builder.Build();

            // NOTE: The last configuration source specified “wins”

            // TODO: Inspect this and uncoment lines bellow
            // chain calls together as a fluent API
            //var config = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json")
            //.AddEntityFrameworkConfig(options =>
            //options.UseSqlServer(connectionStringConfig.GetConnectionString("DefaultConnection"))
            //)
            //.Build();
        }

        public static void ChainCallTogether()
        {
            var memorySettings = new Dictionary<string, string>();
            memorySettings.Add("ConnectionStrings:Northwind", "server=SomeServer; database=Northwind;Integrated Security=true;");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettingsOther.json", optional: true, reloadOnChange: true)
                .Build();

            // NOTE: Order how config files are chanined is important. Last config wins.

            Console.WriteLine(config["ConnectionStrings:Northwind"]);
        }

        public static void EnviromentReplacement()
        {
            var enviromentName = "prod";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{enviromentName}.json", optional: true);

            var config = builder.Build();

            Console.WriteLine(config["ConnectionStrings:Northwind"]);
        }

        public static void EnvironmentReplacementTwo()
        {
            IHostingEnvironmentExample(null); // TODO: fix this
        }

        public static void IHostingEnvironmentExample(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
        }

        public static void OrderPredenceAddCommandLine()
        {
            var args = new[] { "username=Fero" }; // NOTE: For more details about parsing see source code aspnet/configuration tests

            var builder = new ConfigurationBuilder();

            Console.WriteLine("Initial Config Sources: {0}", builder.Sources.Count());

            // This will create initial keys
            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "username", "Guest" }
            });

            Console.WriteLine("Added Memory Source. Sources: {0}", builder.Sources.Count());

            // Will import argumens and overwrite default values. NOTE: Arguments order is important
            builder.AddCommandLine(args);

            Console.WriteLine("Added Command Line Source. Sources: {0}", builder.Sources.Count());

            var config = builder.Build();
            string username = config["username"];

            Console.WriteLine($"Hello, {username}!");
        }

        // Using Options and configuration objects

        public static void OptionsSample()
        {
            var settings = new Dictionary<string, string>
            {
                { "Northwind", "server=(local); database=Northwind;Integrated Security=true;" }
            };

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddInMemoryCollection(settings);

            var config = builder.Build();

            var options = new ConnectionStrings();
            ConfigurationBinder.Bind(config, options);

            Console.WriteLine(options.Northwind);
        }

        public static void OptionsTwoSample()
        {
            // Arrange
            var services = new ServiceCollection().AddOptions();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var config = builder.Build();
            
            // Using sub section from .json file
            services.Configure<ConnectionStrings>(strings =>
            {
                strings.Northwind = config.GetValue<string>("ConnectionStrings:Northwind");
            });

            var connectionStringInFile = services.BuildServiceProvider().GetService<IOptions<ConnectionStrings>>().Value;

            Console.WriteLine(connectionStringInFile.Northwind);

            // In code configuration

            services.Configure<ConnectionStrings>(myOptions =>
            {
                myOptions.Northwind = "Connection string here...";
            });

            var connectionStringInCode = services.BuildServiceProvider().GetService<IOptions<ConnectionStrings>>().Value;

            Console.WriteLine(connectionStringInCode.Northwind);
        }

        // Writing custom providers

        public static void CustomProviderSample()
        {
            // work with with a builder using multiple calls
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var connectionStringConfig = builder.Build();

            // chain calls together as a fluent API
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddCustomConfig()
                .Build();

            Console.WriteLine("key1={0}", config["key1"]);
            Console.WriteLine("key2={0}", config["key2"]);
        }
    }

    public class ConnectionStrings
    {
        public string Northwind { get; set; }
    }

    public class CustomConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CustomConfigurationProvider();
        }
    }

    public class CustomConfigurationProvider : ConfigurationProvider, IEnumerable<KeyValuePair<string, string>>
    {

        public override void Load()
        {
            // were load some settings from database or wherelse
            this.Data.Add("key1", "1");
            this.Data.Add("key2", "2");
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    public static class CustomFrameworkExtensions
    {
        public static IConfigurationBuilder AddCustomConfig(this IConfigurationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Add(new CustomConfigurationSource());
            return builder;
        }
    }
}