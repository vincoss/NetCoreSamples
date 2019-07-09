using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Blazor_GetStarted_ClientSide_V3.Components
{
    /*
        <!-- Menu.razor -->

        <nav class="menu">
            <ul>
                <li><NavLink href="/" Match="NavLinkMatch.All">Home</NavLink></li>
                <li><NavLink href="/contact">Contact</NavLink></li>
            </ul>
        </nav> 

        ### Use another component from C#
        builder.OpenComponent<ManualMenuRender>(0);
        builder.CloseComponent();
    */

    public class ManualMenuRender : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(0, "nav");
            builder.AddAttribute(1, "class", "menu");

            builder.OpenElement(2, "ul");
            builder.OpenElement(3, "li");
            builder.OpenComponent<NavLink>(4);
            builder.AddAttribute(5, "href", "/");
            builder.AddAttribute(6, "Match", NavLinkMatch.All);
            builder.AddAttribute(7, "ChildContent", (RenderFragment)((builder2) => {
                builder2.AddContent(8, "Home");
            }));
            builder.CloseComponent();
            builder.CloseElement();

            builder.OpenElement(9, "li");
            builder.OpenComponent<NavLink>(10);
            builder.AddAttribute(11, "href", "/contact");
            builder.AddAttribute(12, "ChildContent", (RenderFragment)((builder2) => {
                builder2.AddContent(13, "Contact");
            }
            ));
            builder.CloseComponent();
            builder.CloseElement();
            builder.CloseElement();
            builder.CloseElement();
        }
    }
}
