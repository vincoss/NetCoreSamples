using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OData_Samples.Filters;
using OData_Samples.Models;

namespace OData_Samples
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();
            // Enable OData query options globaly. or use [EnableQuery] attribute for individual actions
            var queryAttribute = new EnableQueryAttribute()
            {
                AllowedQueryOptions = AllowedQueryOptions.Top | AllowedQueryOptions.Skip,
                MaxTop = 100
            };
            services.AddODataQueryFilter(queryAttribute);// http://go.microsoft.com/fwlink/?LinkId=279712
            /*
                OR custom filter, default filter
                services.AddODataQueryFilter(new CustomQueryableAttribute());
                services.AddODataQueryFilter();
            */

            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(b => b.MapODataServiceRoute("service", "service", GetEdmModelFooBarRec()));
        }
        
        private static IEdmModel GetEdmModelFooBarRec()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<FooBarRec>("FooBarRecs")
                .EntityType
                .HasKey(x => x.Id)
                /*
                    Define a key for the OData mapping. or use the [Key] attribute.
                    Exception: Web APi OData V4 Issue "The entity '' does not have a key defined
                */
                .Filter()   // Allow for the $filter Command
                .Count()    // Allow for the $count Command
                .Expand()   // Allow for the $expand Command
                .OrderBy()  // Allow for the $orderby Command
                .Page()     // Allow for the $top and $skip Commands
                .Select();  // Allow for the $select Command

            builder.EntitySet<Foo>("Foos")
                .EntityType
                .Filter()
                .Count()
                .Expand()
                .OrderBy()
                .Page()
                .Select();

            builder.EntitySet<Bar>("Bars")
                .EntityType
                .Filter()
                .Count()
                .Expand()
                .OrderBy()
                .Page()
                .Select();

            builder.EntitySet<Keyword>("Keywords")
                .EntityType
                .HasKey(x => x.Name)
                .Filter()
                .Count()
                .Expand()
                .OrderBy()
                .Page()
                .Select();

            builder.EntitySet<Variable>("Variables")
               .EntityType
               .HasKey(x => x.Name)
               .Filter()
               .Count()
               .Expand()
               .OrderBy()
               .Page()
               .Select();

            builder.EntitySet<Product>("Products")
              .EntityType
              .HasKey(x => x.ID)
              .Filter()
              .Count()
              .Expand()
              .OrderBy()
              .Page()
              .Select();

            builder.EntitySet<Supplier>("Suppliers")
             .EntityType
             .HasKey(x => x.Key)
             .Filter()
             .Count()
             .Expand()
             .OrderBy()
             .Page()
             .Select();

            builder.EntitySet<Category>("Categories")
             .EntityType
             .HasKey(x => x.ID)
             .Filter()
             .Count()
             .Expand()
             .OrderBy()
             .Page()
             .Select();

            return builder.GetEdmModel();
        }

        private static IEdmModel GetEdmModelIgnoreDataMemberFoo()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<IgnoreDataMemberFoo>("IgnoreDataMemberFoos")
                .EntityType
                .HasKey(x => x.Name)
                .Filter()
                .Count()
                .Expand()
                .OrderBy()
                .Page()
                .Select()
                .Ignore(x => x.Age); // will not be visible to query

            return builder.GetEdmModel();
        }
    }
}
