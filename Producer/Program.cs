using Infrastructure;
using Infrastructure.Filters;
using MassTransit;
using Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddTransient<Token>();

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", 5672, "/", hostConfig =>
        {
            hostConfig.Username("guest");
            hostConfig.Password("guest");
        });

        cfg.UseRawJsonSerializer();
        cfg.UseRawJsonDeserializer();

        cfg.UseSendFilter(typeof(TokenSendFilter<>), context);
        cfg.UsePublishFilter(typeof(TokenPublishFilter<>), context);
        cfg.UseConsumeFilter(typeof(TokenConsumeFilter<>), context);
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