using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Security_AppSecretsWithoutAzure
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Program.AddDataProtection(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction())
            {
                // extract your secreets 
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var dataProtectionProvider = app.ApplicationServices.GetService<IDataProtectionProvider>();
                    var protector = Program.CreateProtector(dataProtectionProvider);
                    var secretEnc = Configuration["SampleSecret"];
                    var secretVal = protector.Unprotect(secretEnc);
                    var messageEnc = Configuration["Message"];
                    var messageVal = protector.Unprotect(messageEnc);

                    await context.Response.WriteAsync($"Hello World!-{secretVal}-{messageVal}");
                });
            });
        }
    }
}
