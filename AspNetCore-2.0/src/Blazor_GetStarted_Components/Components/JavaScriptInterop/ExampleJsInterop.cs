using Blazor_GetStarted_Components.Data;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_GetStarted_Components.Components.JavaScriptInterop
{
    public class ExampleJsInterop
    {
        private readonly IJSRuntime _jsRuntime;

        public ExampleJsInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public Task CallHelloHelperSayHello(string name)
        {
            // sayHello is implemented in wwwroot/exampleJsInterop.js
            return _jsRuntime.InvokeAsync<object>(
                "exampleJsFunctions.sayHello",
                 DotNetObjectRef.Create(new HelloHelper(name)));
        }
    }
}
