using Fundamentals_Options.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fundamentals_Options.Services
{
    public class SampleHostedService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IApplicationLifetime _applicationLifetime;

        public readonly IOptions<MyOptions> _options;
        public readonly IOptionsMonitor<MyOptions> _optionsAccessor;
        private readonly MyOptionsWithDelegateConfig _optionsWithDelegateConfig;
        public readonly MySubOptions _subOptions;
        public readonly MyOptions _snapshotOptions;
        public readonly MyOptions _named_options_1;
        public readonly MyOptions _named_options_2;

        public SampleHostedService(
           IConfiguration configuration,
           IHostingEnvironment hostingEnvronment,
           IApplicationLifetime applicationLifetime,
           IOptions<MyOptions> options,
           IOptionsMonitor<MyOptions> optionsAccessor,
           IOptionsMonitor<MyOptionsWithDelegateConfig> optionsAccessorWithDelegateConfig,
           IOptionsMonitor<MySubOptions> subOptionsAccessor,
           IOptionsSnapshot<MyOptions> snapshotOptionsAccessor,
           IOptionsSnapshot<MyOptions> namedOptionsAccessor)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvronment;
            _applicationLifetime = applicationLifetime;
            _options = options;
            _optionsAccessor = optionsAccessor;
            _optionsWithDelegateConfig = optionsAccessorWithDelegateConfig.CurrentValue;
            _subOptions = subOptionsAccessor.CurrentValue;
            // Reload configuration data with IOptionsSnapshot
            _snapshotOptions = snapshotOptionsAccessor.Value;
            // Named options support with IConfigureNamedOptions
            _named_options_1 = namedOptionsAccessor.Get("named_options_1");
            _named_options_2 = namedOptionsAccessor.Get("named_options_2");

            _optionsAccessor.OnChange(OnOptionsChange);
        }

        public void OnOptionsChange(MyOptions options, string listener)
        {
            Console.WriteLine($"OnOptionsChange - {options.Option1}, {options.Option2}");
        }

        /// <summary>
        /// Access the options.
        /// </summary>
        private void DisplayInformation()
        {
            Console.WriteLine($"ApplicationName: {_hostingEnvironment.ApplicationName}");
            Console.WriteLine($"ContentRootFileProvider: {_hostingEnvironment.ContentRootFileProvider}");
            Console.WriteLine($"ContentRootPath: {_hostingEnvironment.ContentRootPath}");
            Console.WriteLine($"EnvironmentName: {_hostingEnvironment.EnvironmentName}");

            Console.WriteLine($"{_options.Value.Option1}, {_options.Value.Option2}");
            Console.WriteLine($"Options accessor: {_optionsAccessor.CurrentValue.Option1}, {_optionsAccessor.CurrentValue.Option2}");
            Console.WriteLine($"{_optionsWithDelegateConfig.Option1}, {_optionsWithDelegateConfig.Option2}");

            // Options Validation
            OptionsValidationSample();

            // _applicationLifetime.StopApplication();
        }

        public void OptionsValidationSample()
        {
            // Read option
            try
            {
                var options = _optionsAccessor.Get("optionalOptionsName");
            }
            catch (OptionsValidationException e)
            {
                foreach (var item in e.Failures)
                {
                    Console.WriteLine(item);
                }
                // e.OptionsName returns "optionalOptionsName"
                // e.OptionsType returns typeof(MyOptions)
                // e.Failures returns a list of errors, which would contain 
                //     "custom error"
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            DisplayInformation();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
