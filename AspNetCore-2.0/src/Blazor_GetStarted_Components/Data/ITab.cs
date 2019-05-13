using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_GetStarted_Components.Data
{
    public interface ITab
    {
        RenderFragment ChildContent { get; }
    }
}
