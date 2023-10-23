using MassTransit;

var builder = WebApplication.CreateBuilder();

builder.Services.AddMassTransit(config =>
{
    config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("consumer--", false));
    config.AddConsumers(typeof(Program).Assembly);

    config.UsingRabbitMq((ctx, rmqConfig) =>
    {
        rmqConfig.Host("localhost", 5672, "/", hostConfig =>
        {
            hostConfig.Username("guest");
            hostConfig.Password("guest");
        });

        rmqConfig.ConfigureEndpoints(ctx);
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