using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Fundamentals_MakeHttpRequests.Handlers;
using Fundamentals_MakeHttpRequests.Models;
using Fundamentals_MakeHttpRequests.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Fundamentals_MakeHttpRequests
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Basic usage
            services.AddHttpClient();

            // Named clients
            services.AddHttpClient("github", c =>
            {
                c.BaseAddress = new Uri("https://api.github.com/");
                // Github API versioning
                c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                // Github requires a user-agent
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            });

            // Typed clients
            services.AddHttpClient<GitHubHttpClientService>(c =>
            {
                // Configure in here insted of constructor
                /*
                c.BaseAddress = new Uri("https://api.github.com/");
                c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
                */        
            });

            // Generated clients
            services.AddHttpClient("hello", c =>
            {
                c.BaseAddress = new Uri("http://localhost:5000");
            })
            .AddTypedClient(c => Refit.RestService.For<IHelloClient>(c));

            // Outgoing request middleware
            services.AddTransient<ValidateHeaderHandler>();
            services.AddTransient<SecureRequestHandler>();
            services.AddTransient<RequestDataHandler>();

            services.AddHttpClient("externalservice", c =>
            {
                // Assume this is an "external" service which requires an API KEY
                c.BaseAddress = new Uri("https://localhost:5000/");
            })
            .AddHttpMessageHandler<ValidateHeaderHandler>();

            // Multiple handlers can be registerd in order that they should execute
            services.AddHttpClient("clientwithhandlers")
            // This handler is on the outside and called first during the request, last during the response.
            .AddHttpMessageHandler<SecureRequestHandler>()
            // This handler is on the inside, closest to the request being sent.
            .AddHttpMessageHandler<RequestDataHandler>();

            // #Use Polly-based handlers
            // Handle transient faults
            services.AddHttpClient<UnreliableEndpointCallerService>()
                   .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));

            // Add multiple Polly handlers
            services.AddHttpClient("multiplepolicies")
                    .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                    .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            // Add policies from the Polly registry
            var registry = services.AddPolicyRegistry();

            var timeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));
            var longTimeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30));

            registry.Add("regular", timeout);
            registry.Add("long", longTimeout);

            services.AddHttpClient("regulartimeouthandler")
                    .AddPolicyHandlerFromRegistry("regular");

            // Circuit breaker pattern
            services.AddHttpClient("circuitBreaker")
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                    .AddPolicyHandler(GetRetryPolicy())
                    .AddPolicyHandler(GetCircuitBreakerPolicy());

            // #HttpClient and lifetime management
            services.AddHttpClient("extendedhandlerlifetime").SetHandlerLifetime(TimeSpan.FromMinutes(5));

            // #Configure the HttpMessageHandler
            services.AddHttpClient("configured-inner-handler")
                    .ConfigurePrimaryHttpMessageHandler(() =>
                    {
                        return new HttpClientHandler()
                        {
                            AllowAutoRedirect = false,
                            UseDefaultCredentials = true
                        };
                    });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Place the call to UseDeveloperExceptionPage before any middleware that you want to catch exceptions.
                app.UseDeveloperExceptionPage();

                // Database error page
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
