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
            WebHostSamples.SampleMain(args);

            //CreateWebHostBuilder(args).Build().Run();
            //CreateDefaultBuilder_Details(args);
            //OverrideConfiguration(args);
        }
    }
}
