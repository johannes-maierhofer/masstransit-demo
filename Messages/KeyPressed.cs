using MassTransit;

namespace Messages
{
    // EntityName is an optional attribute used to override the default entity name for a message type. 
    // https://masstransit.io/documentation/configuration/topology/message#attributes

    [EntityName("key-pressed")]
    public record KeyPressed(string Key);
}
