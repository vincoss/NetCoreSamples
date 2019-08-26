using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApps_RazorSyntax_V3.Services
{
    public class DataService
    {
        public Task<IEnumerable<string>> GetCSharpKeywords()
        {
            var keywordk = ExtensionsHelpers.GetEmbeddedContent("WebApps_RazorSyntax_V3.Data.CSharpKeywords.txt");
            var items = keywordk.Split(Environment.NewLine);
            return Task.FromResult(items.AsEnumerable());
        }
    }
}
