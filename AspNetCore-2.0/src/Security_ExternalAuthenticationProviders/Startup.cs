using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security_ExternalAuthenticationProviders.Data;
using Security_ExternalAuthenticationProviders.Models;
using Security_ExternalAuthenticationProviders.Services;
using Microsoft.AspNetCore.Http;

namespace Security_ExternalAuthenticationProviders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // See: %APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json
            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ApplicationId"];
                microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:Password"];
            });

            /*
                services.AddAuthentication()
                        .AddMicrosoftAccount(microsoftOptions => { ... })
                        .AddGoogle(googleOptions => { ... })
                        .AddTwitter(twitterOptions => { ... })
                        .AddFacebook(facebookOptions => { ... }); 
            */

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();


            /*
                Require HTTPS
            */
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect; // Production apps should call UseHsts.
                options.HttpsPort = 5001;   // Sets the HTTPS port to 5001. The default value is 443.
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // OAuth 2.0 requires the use of SSL for authentication over the HTTPS protocol.
            }

            var builder = new ConfigurationBuilder()
                  .SetBasePath(env.ContentRootPath)
                  .AddJsonFile("appsettings.json",
                               optional: false,
                               reloadOnChange: true)
                  .AddEnvironmentVariables();

                        if (env.IsDevelopment())
                        {
                            builder.AddUserSecrets<Startup>();
                        }

            app.UseStaticFiles();
            // Require HTTPS
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
