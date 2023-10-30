using Azure.Messaging.ServiceBus;
using MassTransit;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddMassTransit(config =>
{
    config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("consumer--", false));
    config.AddConsumers(typeof(Program).Assembly);

    config.UsingAzureServiceBus((ctx, azureSbConfig) =>
    {
        azureSbConfig.Host("YOUR-PRIMARY-CONNECTION-STRING",
            cfg =>
            {
                cfg.TransportType = ServiceBusTransportType.AmqpWebSockets;
            });

        azureSbConfig.ConfigureEndpoints(ctx);
    });
});

var app = builder.Build();
await app.StartAsync();

Console.WriteLine("Consumer started.");

ConsoleKeyInfo cki;
do
{
    cki = Console.ReadKey();
} while (cki.Key != ConsoleKey.Escape);

await app.StopAsync();
Console.WriteLine("Consumer stopped.");