﻿using Microsoft.JSInterop;
using System.Threading.Tasks;

public class JsInteropClasses
{
    private readonly IJSRuntime _jsRuntime;

    public JsInteropClasses(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public Task<string> TickerChanged(string data)
    {
        // The handleTickerChanged JavaScript method is implemented
        // in a JavaScript file, such as 'wwwroot/tickerJsInterop.js'.
        return _jsRuntime.InvokeAsync<string>(
            "handleTickerChanged",
            "$",
            3.14D);
    }

    //// Or
    //[Inject]
    //IJSRuntime JSRuntime { get; set; }
}