using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_TheHost.Services
{
    public class CustomFileReader
    {
        private readonly IHostingEnvironment _env;

        public CustomFileReader(IHostingEnvironment env)
        {
            _env = env;
        }

        public string ReadFile(string filePath)
        {
            var fileProvider = _env.WebRootFileProvider;
            return "LOL";
            // Process the file here
        }
    }
}
