using MassTransit;
using Messages;

var builder = WebApplication.CreateBuilder();

builder.Services.AddMassTransit(config =>
{
    config.UsingAzureServiceBus((_, azureSbConfig) =>
    {
        azureSbConfig.Host("Your-Primary-Connection-String-from-Azure-ServiceBus");
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
        var bus = app.Services.GetRequiredService<IBus>();
        await bus.Publish(new KeyPressed(cki.Key.ToString()));
        
        Console.WriteLine();
        Console.WriteLine($"Published KeyPressed event for key '{cki.Key}'.");
    }

    cki = Console.ReadKey();
} 

await app.StopAsync();
Console.WriteLine("Producer stopped.");