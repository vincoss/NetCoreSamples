using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace WebApps_jQuery_Samples.Services
{
    public static class UtilityExtensions
    {
        private static Random _rnd = new Random();

        public static string GetEmbeddedContent(string resourceName)
        {
            if(string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentNullException(nameof(resourceName));
            }

            var assembly = typeof(UtilityExtensions).Assembly;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }

        public static string GetRandomColor()
        {
            var color = String.Format("#{0:X6}", _rnd.Next(0x1000000));
            return color;
        }
    }
}
