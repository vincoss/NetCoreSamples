using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApps_RazorPages_RouteAndAppConventions.Conventions;
using WebApps_RazorPages_RouteAndAppConventions.Data;
using WebApps_RazorPages_RouteAndAppConventions.Factories;
using WebApps_RazorPages_RouteAndAppConventions.Filters;

namespace WebApps_RazorPages_RouteAndAppConventions
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions(options =>
                {
                    /*
                    // 1. Example

                        options.Conventions.Add(... );
                        options.Conventions.AddFolderRouteModelConvention("/OtherPages", model => {  });
                        options.Conventions.AddPageRouteModelConvention("/About", model => {  });
                        options.Conventions.AddPageRoute("/Contact", "TheContactPage/{text?}");
                        options.Conventions.AddFolderApplicationModelConvention("/OtherPages", model => {  });
                        options.Conventions.AddPageApplicationModelConvention("/About", model => {  });
                        options.Conventions.ConfigureFilter(model => { return null; });
                    */

                    // 2. Route order
                    options.Conventions.Add(new GlobalTemplatePageRouteModelConvention());

                    // 3. Add an app model convention to all pages
                    options.Conventions.Add(new GlobalHeaderPageApplicationModelConvention());

                    // 4. Add a handler model convention to all pages
                    options.Conventions.Add(new GlobalPageHandlerModelConvention());

                    // #Page route action conventions
                    // 5. Folder route model convention
                    options.Conventions.AddFolderRouteModelConvention("/OtherPages", model =>
                    {
                        var selectorCount = model.Selectors.Count;
                        for (var i = 0; i < selectorCount; i++)
                        {
                            var selector = model.Selectors[i];
                            model.Selectors.Add(new SelectorModel
                            {
                                AttributeRouteModel = new AttributeRouteModel
                                {
                                    Order = 2,
                                    Template = AttributeRouteModel.CombineTemplates(selector.AttributeRouteModel.Template, "{otherPagesTemplate?}"),
                                }
                            });
                        }
                    });

                    // 6. Page route model convention
                    options.Conventions.AddPageRouteModelConvention("/About", model =>
                    {
                        var selectorCount = model.Selectors.Count;
                        for (var i = 0; i < selectorCount; i++)
                        {
                            var selector = model.Selectors[i];
                            model.Selectors.Add(new SelectorModel
                            {
                                AttributeRouteModel = new AttributeRouteModel
                                {
                                    Order = 2,
                                    Template = AttributeRouteModel.CombineTemplates(
                                        selector.AttributeRouteModel.Template,
                                        "{aboutTemplate?}"),
                                }
                            });
                        }
                    });

                    // # 7. Use a parameter transformer to customize page routes
                    options.Conventions.Add(new PageRouteTransformerConvention(new SlugifyParameterTransformer()));

                    // # 8. Configure a page route
                    options.Conventions.AddPageRoute("/Contact", "TheContactPage/{text?}");

                    // # Page model action conventions
                    // 9. Folder app model convention
                    options.Conventions.AddFolderApplicationModelConvention("/OtherPages", model =>
                    {
                        model.Filters.Add(new AddHeaderAttribute("OtherPagesHeader", new string[] { "OtherPages Header Value" }));
                    });

                    // 10. Page app model convention
                    options.Conventions.AddPageApplicationModelConvention("/About", model =>
                    {
                        model.Filters.Add(new AddHeaderAttribute("AboutHeader", new string[] { "About Header Value" }));
                    });

                    // 11. Configure a filter
                    options.Conventions.ConfigureFilter(model =>
                    {
                        if (model.RelativePath.Contains("OtherPages/Page2"))
                        {
                            return new AddHeaderAttribute("OtherPagesPage2Header", new string[] { "OtherPages/Page2 Header Value" });
                        }
                        return new EmptyFilter();
                    });

                    // 12. Configure a filter factory
                    options.Conventions.ConfigureFilter(new AddHeaderWithFactory());
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc();
        }
    }
}
