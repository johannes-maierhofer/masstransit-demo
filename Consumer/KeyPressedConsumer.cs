using MassTransit;
using Messages;
using Microsoft.Extensions.Logging;

namespace Consumer
{
    public class KeyPressedConsumer : IConsumer<KeyPressed>
    {
        private readonly ILogger<KeyPressedConsumer> _logger;

        public KeyPressedConsumer(ILogger<KeyPressedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<KeyPressed> context)
        {
            _logger.LogInformation("Consuming KeyPressed event with key '{Key}'.", context.Message.Key);
            return Task.CompletedTask;
        }
    }
}
