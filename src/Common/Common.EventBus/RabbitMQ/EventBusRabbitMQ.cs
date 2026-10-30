using System.Text;
using Common.EventBus.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Common.EventBus.RabbitMQ;

public class EventBusRabbitMQ : IEventBus
{
    private readonly string _hostName;
    private readonly string _password;
    private readonly string _userName;
    private readonly ILogger<EventBusRabbitMQ> _logger;

    public EventBusRabbitMQ(IConfiguration configuration, ILogger<EventBusRabbitMQ> logger)
    {
        _logger = logger;
        _hostName = configuration.GetSection("RabbitMQ")["HostName"] ?? "localhost";
        _userName = configuration.GetSection("RabbitMQ")["UserName"] ?? "guest";
        _password = configuration.GetSection("RabbitMQ")["Password"] ?? "guest";
    }

    private IConnection _connection;

    public async Task PublishAsync(
        IntegrationEvent @event,
        string? queueName = null,
        string exchange = "",
        string exchangeType = ExchangeType.Direct,
        string routeKey = "",
        CancellationToken cancellationToken = default)
    {
        var connection = await GetConnectionAsync(cancellationToken);

        await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        if (string.IsNullOrWhiteSpace(routeKey))
        {
            routeKey = queueName ?? string.Empty;
        }

        if (!string.IsNullOrWhiteSpace(queueName))
        {
            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken);

            if (exchangeType == ExchangeType.Direct && !string.IsNullOrWhiteSpace(exchange))
            {
                await channel.QueueBindAsync(
                    queue: queueName,
                    exchange: exchange,
                    routingKey: queueName,
                    cancellationToken: cancellationToken);
            }
        }

        if (!string.IsNullOrWhiteSpace(exchange))
        {
            await channel.ExchangeDeclareAsync(
                exchange: exchange,
                type: exchangeType,
                durable: true,
                autoDelete: false,
                cancellationToken: cancellationToken);
        }

        var json = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties
        {
            DeliveryMode = DeliveryModes.Persistent
        };

        await channel.BasicPublishAsync(
            exchange: exchange,
            routingKey: routeKey,
            mandatory: false,
            basicProperties: properties,
            body: body,
            cancellationToken: cancellationToken);
    }


    private async Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
    {
        if (_connection is { IsOpen: true })
            return _connection;

        try
        {
            if (_connection is { IsOpen: true })
                return _connection;

            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };

            _connection = await factory.CreateConnectionAsync(cancellationToken);
            return _connection;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "خطا در برقراری ارتباط با RabbitMQ: {Message}", ex.Message);
            throw;
        }

    }


}