using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_Configuration.Configuration
{
    public class ConnectionStrings
    {
        public string Northwind { get; set; }
    }

    public class CustomConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CustomConfigurationProvider();
        }
    }

    public class CustomConfigurationProvider : ConfigurationProvider, IEnumerable<KeyValuePair<string, string>>
    {

        public override void Load()
        {
            // were load some settings from database or wherelse
            this.Data.Add("key1", "1");
            this.Data.Add("key2", "2");
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class CustomFrameworkExtensions
    {
        public static IConfigurationBuilder AddCustomConfig(this IConfigurationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Add(new CustomConfigurationSource());
            return builder;
        }
    }
}
