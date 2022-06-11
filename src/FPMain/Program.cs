// See https://aka.ms/new-console-template for more information

using System.IO.Pipelines;
using FPMain;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Connections;

Console.WriteLine("Hello, World!");

short proxyPort = 30121;
short destPort = 30120;

CancellationTokenSource cts = new CancellationTokenSource();

Console.WriteLine("Starting proxy...");

var builder = new WebHostBuilder();
builder.ConfigureKestrel((context, options) =>
{
    options.ListenAnyIP(2020);
    //options.ListenAnyIP(2007, opt => opt.UseConnectionHandler<>());
});

Proxy proxy = new Proxy(proxyPort, destPort);

Console.CancelKeyPress += (sender, eventArgs) =>
{
    Console.WriteLine("Stopping proxy.");
    cts.Cancel();
    eventArgs.Cancel = true;
};


Console.WriteLine("Press Ctrl+C to cancel...");

await proxy.ProxyUdp(cts.Token);

