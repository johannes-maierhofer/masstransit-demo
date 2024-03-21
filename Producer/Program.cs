using MassTransit;
using Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((_, rmqConfig) =>
    {
        // Consuming messages from other systems where messages may not be produced by MassTransit, raw JSON is commonly used.
        // see https://masstransit.io/documentation/configuration/serialization#raw-json
        rmqConfig.UseRawJsonSerializer();

        rmqConfig.Host("localhost", 5672, "/", hostConfig =>
        {
            hostConfig.Username("guest");
            hostConfig.Password("guest");
        });
    });
});

var app = builder.Build();
await app.StartAsync();

Console.WriteLine("Producer started.");
Console.WriteLine("Press any key to publish a message or ESC to exit.");

var cki = Console.ReadKey();
while (cki.Key != ConsoleKey.Escape)
{
    using (app.Services.CreateScope())
    {
        var publishEndpoint = app.Services.GetRequiredService<IPublishEndpoint>();
        await publishEndpoint.Publish(new KeyPressed(cki.Key.ToString()));
        
        Console.WriteLine();
        Console.WriteLine($"Published KeyPressed event for key '{cki.Key}'.");
    }

    cki = Console.ReadKey();
} 

await app.StopAsync();
Console.WriteLine("Producer stopped.");