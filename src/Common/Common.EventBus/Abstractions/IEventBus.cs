using RabbitMQ.Client;

namespace Common.EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(
        IntegrationEvent @event,
        string? queueName = null,
        string exchange = "",
        string exchangeType = ExchangeType.Direct,
        string routeKey = "",
        CancellationToken cancellationToken = default);

}