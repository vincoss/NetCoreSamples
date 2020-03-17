using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Dynamic;

namespace Security_AppSecretsWithoutAzure
{
    /// <summary>
    /// dotnet Security_AppSecretsWithoutAzure.dll -config "-v=Message=Hello World!" "-v=SampleSecret=secret"
    /// </summary>
    public class Program
    {
        private const string APP_NAME = "DA83644B-7EF6-4132-8B4D-3EAD1E70CBB1";
        private const string SECRET_CONFIG_FILE_NAME = "appsettings.secret.json";
        private static readonly IDictionary<string, string> _variables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public static void Main(string[] args)
        {
            if (args != null && args.Any(x => x.Contains("-config")))
            {
                foreach(var a in args)
                {
                    if(a.Contains("-config"))
                    {
                        continue;
                    }
                    var str = a.Trim().Replace("-v=", "");
                    if(a.Length <= 0)
                    {
                        continue;
                    }
                    AddVariable(str, _variables);
                }

                ConfigAppSettingsSecret();
                return;
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseConfiguration(GetConfiguration());
                });

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.secret.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            return builder.Build();
        }

        private static void ConfigAppSettingsSecret()
        {
            var serviceCollection = new ServiceCollection();
            AddDataProtection(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();
            var dataProtectionProvider = services.GetService<IDataProtectionProvider>();
            var protector = CreateProtector(dataProtectionProvider);

            var expando = new ExpandoObject();
            var dict = (IDictionary<string, object>)expando;
            
            foreach(var pair in _variables)
            {
                dict.Add(pair.Key, protector.Protect(pair.Value));
            }

            string json = JsonSerializer.Serialize(expando);
            var path = ConfigFileFullPath;
            File.WriteAllText(path, json);
            Console.WriteLine($"Writing app settings secret to '${path}' completed successfully.");
        }

        private static string CurrentDirectory
        {
            get { return Directory.GetParent(typeof(Program).Assembly.Location).FullName; }
        }

        private static string ConfigFileFullPath
        {
            get { return Path.Combine(CurrentDirectory, SECRET_CONFIG_FILE_NAME); }
        }

        internal static void AddDataProtection(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDataProtection()
                .SetApplicationName(APP_NAME)
                .DisableAutomaticKeyGeneration();
        }

        internal static IDataProtector CreateProtector(IDataProtectionProvider dataProtectionProvider)
        {
            return dataProtectionProvider.CreateProtector(APP_NAME);
        }

        private static byte[] ReadPasswordFromConsole()
        {
            Console.WriteLine("Enter a password.");
            var str = Console.ReadLine();
            return Encoding.UTF8.GetBytes(str);
        }

        private static void AddVariable(string value, IDictionary<string, string> variables)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            if (value.IndexOf('=') == -1)
            {
                Console.WriteLine("Please enter correct form of variable name and variable value. -v=name=value");
            }
            string[] array = value.Split(new char[] { '=' });
            variables[array[0]] = ((array.Length > 1) ? array[1] : "");
        }
    }
}
