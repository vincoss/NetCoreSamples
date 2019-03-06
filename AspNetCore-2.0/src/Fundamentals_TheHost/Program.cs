using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.HostFiltering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;


namespace Fundamentals_TheHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            //CreateDefaultBuilder_Details(args);
            //OverrideConfiguration(args);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        // Set up a host

        public static void CreateDefaultBuilder_Details(string[] args)
        {
            var builder = new WebHostBuilder();

            if (string.IsNullOrEmpty(builder.GetSetting(WebHostDefaults.ContentRootKey)))
            {
                builder.UseContentRoot(Directory.GetCurrentDirectory());
            }
            if (args != null)
            {
                builder.UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build());
            }

            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                if (env.IsDevelopment())
                {
                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    if (appAssembly != null)
                    {
                        config.AddUserSecrets(appAssembly, optional: true);
                    }
                }

                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
                logging.AddEventSourceLogger();
            }).
            UseDefaultServiceProvider((context, options) =>
            {
                options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
            });

            // Defaults

            builder.UseKestrel((builderContext, options) =>
            {
                options.Configure(builderContext.Configuration.GetSection("Kestrel"));
            })
           .ConfigureServices((hostingContext, services) =>
           {
                // Fallback
                services.PostConfigure<HostFilteringOptions>(options =>
               {
                   if (options.AllowedHosts == null || options.AllowedHosts.Count == 0)
                   {
                        // "AllowedHosts": "localhost;127.0.0.1;[::1]"
                        var hosts = hostingContext.Configuration["AllowedHosts"]?.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        // Fall back to "*" to disable.
                        options.AllowedHosts = (hosts?.Length > 0 ? hosts : new[] { "*" });
                   }
               });
                // Change notification
                services.AddSingleton<IOptionsChangeTokenSource<HostFilteringOptions>>(new ConfigurationChangeTokenSource<HostFilteringOptions>(hostingContext.Configuration));

             //  services.AddTransient<IStartupFilter, HostFilteringStartupFilter>(); this is internal so check source instead

               services.AddRouting();
           })
           .UseIIS()
           .UseIISIntegration();

            // Startup
            builder.UseStartup<Startup>();

            // Build & Run
            builder.Build().Run();
        }

        // Host configuration values

        public static void HostConfigurationValues(string[] args)
        {
            // Application Key (Name)
            var builder = CreateWebHostBuilder(args);
            builder.UseSetting(WebHostDefaults.ApplicationKey, "CustomApplicationName");

            // Capture Startup Errors
            builder.CaptureStartupErrors(true);

            // Content Root
            builder.UseContentRoot("c:\\<content-root>");

            // Detailed Errors
            builder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");

            // Enviroment
            builder.UseEnvironment(EnvironmentName.Development);

            // Hosting Startup Assemblies
            builder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "assembly1;assembly2");

            // HTTPS Port
            builder.UseSetting("https_port", "8080");

            // Hosting Startup Exclude Assemblies
            builder.UseSetting(WebHostDefaults.HostingStartupExcludeAssembliesKey, "assembly1;assembly2");

            // Prefer Hosting URLs
            builder.PreferHostingUrls(false);

            // Prevent Hosting Startup
            builder.UseSetting(WebHostDefaults.PreventHostingStartupKey, "true");

            // Server URLs
            builder.UseUrls("http://*:5000;https://*:5000;http://localhost:5001;https://hostname:5002");

            // Shutdown Timeout
            builder.UseShutdownTimeout(TimeSpan.FromSeconds(10));

            // Startup Assembly
            builder.UseStartup("StartupAssemblyName");
            //builder.UseStartup<TStartup>();

            // Web Root
            builder.UseWebRoot("public");

        }

        // Override configuration

        public static void OverrideConfiguration(string[] args)
        {
            var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("hostsettings.json", optional: true)
           .AddCommandLine(args)
           .Build();

            var host = WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5000")
                .UseConfiguration(config)
                .Configure(app =>
                {
                    app.Run(context => context.Response.WriteAsync("Hello, World!"));
                });

            host.Build().Run();
        }

        // Manage the host

        public static void ManageTheHost(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            // Run (block the calling thread)
            host.Run();

            // Start (non blocking)
            using (host)
            {
                host.Start();
                Console.ReadLine();
            }

            var urls = new List<string>()
            {
                "http://*:5000",
                "http://localhost:5001"
            };

            var host1 = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Start(urls.ToArray());

            using (host1)
            {
                Console.ReadLine();
            }

            // Start(RequestDelegate app)

            using (var host2 = WebHost.Start(app => app.Response.WriteAsync("Hello, World!")))
            {
                Console.WriteLine("Use Ctrl-C to shutdown the host...");
                host2.WaitForShutdown();
            }

            using (var host2 = WebHost.Start("http://localhost:8080", app => app.Response.WriteAsync("Hello, World!")))
            {
                Console.WriteLine("Use Ctrl-C to shutdown the host...");
                host2.WaitForShutdown();
            }

            // Start(Action<IRouteBuilder> routeBuilder)

            using (var host2 = WebHost.Start(router => 
            router.MapGet("hello/{name}", (req, res, data) =>
            res.WriteAsync($"Hello, {data.Values["name"]}!")).MapGet("buenosdias/{name}", (req, res, data) =>
            res.WriteAsync($"Buenos dias, {data.Values["name"]}!")).MapGet("throw/{message?}", (req, res, data) =>
            throw new Exception((string)data.Values["message"] ?? "Uh oh!")).MapGet("{greeting}/{name}", (req, res, data) => 
            res.WriteAsync($"{data.Values["greeting"]}, {data.Values["name"]}!"))
            .MapGet("", (req, res, data) => res.WriteAsync("Hello, World!"))))
            {
                Console.WriteLine("Use Ctrl-C to shutdown the host...");
                host2.WaitForShutdown();
            }

            // Start(string url, Action<IRouteBuilder> routeBuilder)

            using (var host2 = WebHost.Start("http://localhost:8080", router => router
            .MapGet("hello/{name}", (req, res, data) =>
                res.WriteAsync($"Hello, {data.Values["name"]}!"))
            .MapGet("buenosdias/{name}", (req, res, data) =>
                res.WriteAsync($"Buenos dias, {data.Values["name"]}!"))
            .MapGet("throw/{message?}", (req, res, data) =>
                throw new Exception((string)data.Values["message"] ?? "Uh oh!"))
            .MapGet("{greeting}/{name}", (req, res, data) =>
                res.WriteAsync($"{data.Values["greeting"]}, {data.Values["name"]}!"))
            .MapGet("", (req, res, data) => res.WriteAsync("Hello, World!"))))
            {
                Console.WriteLine("Use Ctrl-C to shut down the host...");
                host2.WaitForShutdown();
            }

            // StartWith(Action<IApplicationBuilder> app)

            using (var host2 = WebHost.StartWith(app =>
                app.Use(next =>
                {
                    return async context =>
                    {
                        await context.Response.WriteAsync("Hello World!");
                    };
                })))
            {
                Console.WriteLine("Use Ctrl-C to shut down the host...");
                host2.WaitForShutdown();
            }

            // StartWith(string url, Action<IApplicationBuilder> app)

            using (var host2 = WebHost.StartWith("http://localhost:8080", app =>
                app.Use(next =>
                {
                    return async context =>
                    {
                        await context.Response.WriteAsync("Hello World!");
                    };
                })))
            {
                Console.WriteLine("Use Ctrl-C to shut down the host...");
                host2.WaitForShutdown();
            }
        }

        // Scope validation

        public static void ScopeValidation(string[] args)
        {
            // To always validate scopes, including in the Production environment, configure.
            var host = CreateWebHostBuilder(args)
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = true;
                });

            host.Build().Run();
        }
    }
}
