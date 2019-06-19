using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_GetStarted_ClientSide.Components
{
    public class CreatingComponentsRenderPageBase : ComponentBase
    {
        public const string TitleBase = "Hello World - Class Only";

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(1, "h4");
            builder.AddContent(2, TitleBase);
            builder.CloseElement();

            base.BuildRenderTree(builder);
        }
    }
}
