window.WriteCSharpMessageToConsole = () => {
    DotNet.invokeMethodAsync('Blazor_GetStarted_ClientSide_V3', 'GetHelloMessage')
        .then(message => {
            console.log(message + ", static");
        });
};

window.WriteCSharpMessageToConsoleInstance = (instance) => {
    instance.invokeMethodAsync('GetHelloMessageInstance')
        .then((message) => {
            console.log(message);
        });
};