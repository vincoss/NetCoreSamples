using Fundamentals_Options.Models;
using Fundamentals_Options.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Fundamentals_Options1
{
    public class Program
    {
        public static async Task Main(string[] args)
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
                  config.AddJsonFile("appsettings.json", optional: false, true);
                  config.AddCommandLine(args);
              })
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddOptions();
                  // Example #1: General configuration
                  services.Configure<MyOptions>(hostContext.Configuration);

                  // Example #2: Options bound and configured by a delegate
                  services.Configure<MyOptionsWithDelegateConfig>(myOptions =>
                  {
                      myOptions.Option1 = "value1_configured_by_delegate";
                      myOptions.Option2 = 500;
                  });

                  // Example #3: Suboptions
                  // Bind options using a sub-section of the appsettings.json file.
                  services.Configure<MySubOptions>(hostContext.Configuration.GetSection("subsection"));

                  // Example #6: Named options (named_options_1)
                  // Register the ConfigurationBuilder instance which MyOptions binds against.
                  // Specify that the options loaded from configuration are named
                  // "named_options_1".
                  services.Configure<MyOptions>("named_options_1", hostContext.Configuration);

                  // Example #6: Named options (named_options_2)
                  // Specify that the options loaded from the MyOptions class are named
                  // "named_options_2".
                  // Use a delegate to configure option values.
                  services.Configure<MyOptions>("named_options_2", myOptions =>
                  {
                      myOptions.Option1 = "named_options_2_value1_from_action";
                  });

                  // Configure all options with the ConfigureAll method
                  services.ConfigureAll<MyOptions>(myOptions =>
                  {
                      myOptions.Option1 = "ConfigureAll replacement value";
                  });

                  // OptionsBuilder API
                  services.AddOptions<MyOptions>().Configure(o => o.Option1 = "default");
                  services.AddOptions<MyOptions>("optionalName").Configure(o => o.Option1 = "named");

                  // Options validation
                  services.AddOptions<MyOptions>("optionalOptionsName")
                            .Configure(o => { }) // Configure the options
                            .Validate(OptionValidationRule, "Option validation error.");

                  services.AddOptions<AnnotatedOptions>()
                           .Configure(o =>
                           {
                               o.StringLength = "111111";
                               o.IntRange = 10;
                               o.Required = "nowhere";
                           })
                           .ValidateDataAnnotations();

                  // Options post-configuration
                  services.PostConfigure<MyOptions>(myOptions => { myOptions.Option1 = "post_configured_option1_value"; });
                  services.PostConfigure<MyOptions>("named_options_1", myOptions => { myOptions.Option1 = "post_configured_option1_value"; });
                  services.PostConfigureAll<MyOptions>(myOptions => { myOptions.Option1 = "post_configured_option1_value"; });

                  services.AddHostedService<SampleHostedService>();
              })
              .ConfigureLogging((hostingContext, logging) =>
              {
                  logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                  logging.AddConsole();
              });

            var host = builder.Build();
            await host.RunAsync();
        }

        public static bool OptionValidationRule(MyOptions options)
        {
            return false;
        }
    }
}
