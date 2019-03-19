using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Fundamentals_TheHost.Services;
using Microsoft.Extensions.Hosting;


namespace Fundamentals_TheHost
{
    public class GenericHostSamples
    {
        public static int SampleMain(string[] args)
        {
            SetupHost(args);
            return 0;
        }

        // Set up a host

        private static async Task SetupHost(string[] args)
        {
            var host = new HostBuilder().Build();
            await host.RunAsync();
        }

        // Shutdown timeout

        private static async Task ShutdownTimeout(string[] args)
        {
            var host = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<HostOptions>(option =>
                {
                    option.ShutdownTimeout = System.TimeSpan.FromSeconds(20);
                });
            })
            .Build();

            await host.RunAsync();
        }

        // Host configuration

        private static async Task HostConfiguration(string[] args)
        {
            var builder = new HostBuilder();

            // Application key (name)
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                hostingContext.HostingEnvironment.ApplicationName = "CustomApplicationName";
            });

            // Or explicit set
            builder.ConfigureAppConfiguration(c =>
            {
                c.Properties.Add(WebHostDefaults.ApplicationKey, "CustomApplicationName");
            });

            // Content root
            builder.UseContentRoot("c:\\<content-root>");

            // Enviroment
            builder.UseEnvironment(Microsoft.AspNetCore.Hosting.EnvironmentName.Development);

            // ConfigureHostConfiguration
            builder.ConfigureHostConfiguration(configHost =>
            {
                configHost.SetBasePath(Directory.GetCurrentDirectory());
                configHost.AddJsonFile("hostsettings.json", optional: true);
                configHost.AddEnvironmentVariables(prefix: "PREFIX_");
                configHost.AddCommandLine(args);
            });

            // ConfigureAppConfiguration
            builder.ConfigureAppConfiguration((hostContext, configApp) =>
            {
                configApp.SetBasePath(Directory.GetCurrentDirectory());
                configApp.AddJsonFile("appsettings.json", optional: true);
                configApp.AddJsonFile( $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                configApp.AddEnvironmentVariables(prefix: "PREFIX_");
                configApp.AddCommandLine(args);
            });

            // ConfigureServices
            builder.ConfigureServices((hostContext, services) =>
            {
                if (hostContext.HostingEnvironment.IsDevelopment())
                {
                    // Development service configuration
                }
                else
                {
                    // Non-development service configuration
                }

                services.AddHostedService<LifetimeEventsHostedService>();
                services.AddHostedService<TimedHostedService>();
            });

            // ConfigureLogging
            builder.ConfigureLogging((hostContext, configLogging) =>
            {
                configLogging.AddConsole();
                configLogging.AddDebug();
            });

            // UseConsoleLifetime
            builder.UseConsoleLifetime();

            // Container configuration
            builder.UseServiceProviderFactory<ServiceContainer>(new ServiceContainerFactory())
                    .ConfigureContainer<ServiceContainer>((hostContext, container) =>
                    {
                    });

            // Extensibility
           // builder.UseHostedService<TimedHostedService>(); // TODO: continue from here

            var host = builder.Build();
            await host.RunAsync();
        }
    }
}
