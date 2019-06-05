using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WevApps_TagHelpers_V3.TagHelpers
{
    public class AddressTagHelperComponent : TagHelperComponent
    {
        private readonly string _markup;

        public override int Order { get; }

        public AddressTagHelperComponent(string markup = "", int order = 1)
        {
            _markup = markup;
            Order = order;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "address", StringComparison.OrdinalIgnoreCase) && output.Attributes.ContainsName("printable"))
            {
                TagHelperContent childContent = await output.GetChildContentAsync();
                string content = childContent.GetContent();
                output.Content.SetHtmlContent($"<div>{content}<br>{_markup}</div>");
            }
        }
    }
}
