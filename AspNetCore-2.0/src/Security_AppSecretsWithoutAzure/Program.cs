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


namespace Security_AppSecretsWithoutAzure
{
    public class Program
    {
        private const string APP_NAME = "DA83644B-7EF6-4132-8B4D-3EAD1E70CBB1";
        private const string SECRET_CONFIG_FILE_NAME = "appsettings.secret.json";

        public static void Main(string[] args)
        {
            if (args != null && args.Length == 1 && args[0].ToLowerInvariant() == "-config")
            {
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

            var data = new
            {
                SampleSecret = protector.Protect(ReadPasswordFromConsole())
            };
            string json = JsonSerializer.Serialize(data);
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
    }
}
