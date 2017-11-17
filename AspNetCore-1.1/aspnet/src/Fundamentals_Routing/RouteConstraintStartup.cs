using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Fundamentals_Routing
{
    public class RouteConstraintStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Using routing middleware
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private void ExampleRoutes(IApplicationBuilder app)
        {
            var routeBuilder = new RouteBuilder(app);
            
            routeBuilder.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id:int}"); // 123456789, -123456789	Matches any integer

            // Route Constraint Reference
            /*
            
            int	        {id:int}	    123456789, -123456789	        Matches any integer
            bool	    {active:bool}	true, FALSE	                    Matches true or false (case-insensitive)
            datetime	{dob:datetime}	2016-12-31, 2016-12-31 7:32pm	Matches a valid DateTime value (in the invariant culture - see warning)
            decimal	    {price:decimal}	49.99, -1,000.01	            Matches a valid decimal value (in the invariant culture - see warning)
            double	    {weight:double}	1.234, -1,001.01e8	            Matches a valid double value (in the invariant culture - see warning)
            float	    {weight:float}	1.234, -1,001.01e8	            Matches a valid float value (in the invariant culture - see warning)
            guid	    {id:guid}	    CD2C1638-1638-72D5-1638-DEADBEEF1638, {CD2C1638-1638-72D5-1638-DEADBEEF1638}	Matches a valid Guid value
            long	    {ticks:long}	123456789, -123456789	        Matches a valid long value
            
            minlength(value)	{username:minlength(4)}	Rick	        String must be at least 4 characters
            maxlength(value)	{filename:maxlength(8)}	Richard	        String must be no more than 8 characters
            ength(length)	    {filename:length(12)}	somefile.txt	String must be exactly 12 characters long
            length(min,max)	    {filename:length(8,16)}	somefile.txt	String must be at least 8 and no more than 16 characters long
            min(value)	        {age:min(18)}	        19	            Integer value must be at least 18
            max(value)	        {age:max(120)}	        91	            Integer value must be no more than 120
            range(min,max)	    {age:range(18,120)}	    91	            Integer value must be at least 18 but no more than 120
            alpha	            {name:alpha}	        Rick	        String must consist of one or more alphabetical characters (a-z, case-insensitive)

            regex(expression)	{ssn:regex(^\\d{{3}}-\\d{{2}}-\\d{{4}}$)}	123-45-6789	String must match the regular expression (see tips about defining a regular expression)
            required	        {name:required}	Rick	                Used to enforce that a non-parameter value is present during URL generation

            */

            // Regular expressions
            /*
            Regular expression tokens must be escaped.  

            ^\d{3}-\d{2}-\d{4}$	            Regular expression
            ^\\d{{3}}-\\d{{2}}-\\d{{4}}$	Escaped
            ^[a-z]{2}$	                    Regular expression
            ^[[a-z]]{{2}}$	                Escaped

            Example route constraint

            {action:regex(^(list|get|create)$)} Matches actions: list, get, create

             */
        }
    }
}
