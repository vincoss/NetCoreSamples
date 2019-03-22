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

            // Ignore data member
            GetEdmModelIgnoreDataMemberFoo(builder);

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

            builder.EntitySet<ProductRating>("Ratings")
              .EntityType
              .HasKey(x => x.ID)
              .Filter()
              .Count()
              .Expand()
              .OrderBy()
              .Page()
              .Select();

            // Action & Functions
            GetEdmModel_ActionAndFunction(builder);

            // Containment
            GetEdmModel_Containment(builder);

            // Singleton
            GetEdmModel_Singleton(builder);

            // Open Types in OData
            GetEdmModel_OpenTypes(builder);

            // Complex Types
            GetEdmModel_ComplexTypes(builder);

            return builder.GetEdmModel();
        }

        public static void GetEdmModel_ActionAndFunction(ODataModelBuilder builder)
        {
            builder.Namespace = "ProductService";

            // Action
            builder.EntityType<Product>()
              .Action("Rate")
              .Parameter<int>("Rating");

            // Function
            builder.EntityType<Product>().Collection
                .Function("MostExpensive")
                .Returns<double>();

            // Unbound Function
            builder.Function("GetSalesTaxRate")
                .Returns<double>()
                .Parameter<int>("PostalCode");
        }

        public static void GetEdmModel_Containment(ODataModelBuilder builder)
        {
            // Containment
            builder.EntitySet<Account>("Accounts");
            var paymentInstrumentType = builder.EntityType<PaymentInstrument>();
            var functionConfiguration = paymentInstrumentType.Collection.Function("GetCount");
            functionConfiguration.Parameter<string>("NameContains");
            functionConfiguration.Returns<int>();
            //builder.Namespace = typeof(Account).Namespace;
        }

        public static void GetEdmModel_Singleton(ODataModelBuilder builder)
        {
            builder.EntitySet<Employee>("Employees");
            builder.Singleton<Company>("Umbrella");
            //builder.Namespace = typeof(Company).Namespace;

            /*
                // Explicit binding navigation property
                EntitySetConfiguration<Employee> employeesConfiguration = builder.EntitySet<Employee>("Employees"); 
                employeesConfiguration.HasSingletonBinding(c => c.Company, "Umbrella");
             */

        }

        public static void GetEdmModel_OpenTypes(ODataModelBuilder builder)
        {
            builder.EntitySet<BookOt>("Books");
            builder.EntitySet<CustomerOt>("Customers");

            // Build EDM model explicitly example
            /*
                ComplexTypeConfiguration<PressOt> pressType = builder.ComplexType<PressOt>();
                pressType.Property(c => c.Name);
                // ...
                pressType.HasDynamicProperties(c => c.DynamicProperties);

                EntityTypeConfiguration<BookOt> bookType = builder.EntityType<BookOt>();
                bookType.HasKey(c => c.ISBN);
                bookType.Property(c => c.Title);
                // ...
                bookType.ComplexProperty(c => c.Press);
                bookType.HasDynamicProperties(c => c.Properties);

                // ...
                builder.EntitySet<BookOt>("Books");
            */
        }

        public static void GetEdmModel_ComplexTypes(ODataModelBuilder builder)
        {
            builder.EntitySet<Window>("Windows"); // infers the inheritance relationships from the CLR types

            // Build EDM model explicitly example which allows more control
            /*

            EnumTypeConfiguration<Color> color = builder.EnumType<Color>();
            color.Member(Color.Red);
            color.Member(Color.Blue);
            color.Member(Color.Green);
            color.Member(Color.Yellow);

            ComplexTypeConfiguration<Point> point = builder.ComplexType<Point>();
            point.Property(c => c.X);
            point.Property(c => c.Y);

            ComplexTypeConfiguration<Shape> shape = builder.ComplexType<Shape>();
            shape.EnumProperty(c => c.Color);
            shape.Property(c => c.HasBorder);
            shape.Abstract();

            ComplexTypeConfiguration<Triangle> triangle = builder.ComplexType<Triangle>();
            triangle.ComplexProperty(c => c.P1);
            triangle.ComplexProperty(c => c.P2);
            triangle.ComplexProperty(c => c.P2);
            triangle.DerivesFrom<Shape>();

            ComplexTypeConfiguration<Rectangle> rectangle = builder.ComplexType<Rectangle>();
            rectangle.ComplexProperty(c => c.LeftTop);
            rectangle.Property(c => c.Height);
            rectangle.Property(c => c.Weight);
            rectangle.DerivesFrom<Shape>();

            ComplexTypeConfiguration<RoundRectangle> roundRectangle = builder.ComplexType<RoundRectangle>();
            roundRectangle.Property(c => c.Round);
            roundRectangle.DerivesFrom<Rectangle>();

            ComplexTypeConfiguration<Circle> circle = builder.ComplexType<Circle>();
            circle.ComplexProperty(c => c.Center);
            circle.Property(c => c.Radius);
            circle.DerivesFrom<Shape>();

            EntityTypeConfiguration<Window> window = builder.EntityType<Window>();
            window.HasKey(c => c.Id);
            window.Property(c => c.Title);
            window.ComplexProperty(c => c.Shape);

            builder.EntitySet<Window>("Windows");
            */
        }

        private static void GetEdmModelIgnoreDataMemberFoo(ODataModelBuilder builder)
        {
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
        }
    }
}
