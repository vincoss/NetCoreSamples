using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_GetStarted_ClientSide_V3.Services
{
    public static class MessageProvider
    {
        [JSInvokable]
        public static Task GetHelloMessage()
        {
            var message = "Hello from C#";
            return Task.FromResult(message);
        }
    }
}
