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
            var numberOfRetries = context.GetRetryAttempt();
            _logger.LogInformation("Consuming KeyPressed event with key '{Key}' (RetryNo: {retries}).",
                context.Message.Key,
                numberOfRetries);
            
            throw new ConsumerTestException();
            // return Task.CompletedTask;
        }
    }

    //public class KeyPressedDefinition : ConsumerDefinition<KeyPressedConsumer>
    //{
    //    protected override void ConfigureConsumer(
    //        IReceiveEndpointConfigurator endpointConfigurator,
    //        IConsumerConfigurator<KeyPressedConsumer> consumerConfigurator,
    //        IRegistrationContext context)
    //    {
    //        consumerConfigurator.UseMessageRetry(r => r.Exponential(
    //            3,
    //            TimeSpan.FromSeconds(1),
    //            TimeSpan.FromSeconds(5),
    //            TimeSpan.FromSeconds(1)));
            
    //        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    //    }
    //}

    //// consuming faults
    //// https://masstransit-project.com/usage/exceptions.html#consuming-faults
    //public class KeyPressedFault : IConsumer<Fault<KeyPressed>>
    //{
    //    private readonly ILogger<KeyPressedFault> _logger;

    //    public KeyPressedFault(ILogger<KeyPressedFault> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public Task Consume(ConsumeContext<Fault<KeyPressed>> context)
    //    {
    //        _logger.LogInformation("Handle faulted KeyPressed event for message with Key '{Key}'.", context.Message.Message.Key);
    //        return Task.CompletedTask;
    //    }
    //}
}
