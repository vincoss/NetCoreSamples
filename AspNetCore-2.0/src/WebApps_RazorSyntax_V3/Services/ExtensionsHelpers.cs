using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps_RazorSyntax_V3.Services
{
    public static class ExtensionsHelpers
    {
        public static string GetEmbeddedContent(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            using (var reader = new StreamReader(typeof(Extensions).Assembly.GetManifestResourceStream(path)))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
