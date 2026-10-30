using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Infrastructure.Persistent;
using CoreModule.Infrastructure.Persistent.EF;
using CoreModule.Infrastructure.Persistent.EF.UserAgg;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.EventHandlers;

public class UserRegisteredEventHandler : BackgroundService
{
    private readonly IEventBus _eventBus;
    private string _queueName = "coreModuleUserRegistered";
    private readonly IServiceScopeFactory _serviceFactory;

    private readonly IConfiguration _configuration;
    private readonly ILogger<UserRegisteredEventHandler> _logger;
    private IConnection? _connection;
    private IChannel? _channel;

    public UserRegisteredEventHandler(IEventBus eventBus, ILogger<UserRegisteredEventHandler> logger, IServiceScopeFactory serviceFactory, IConfiguration configuration)
    {
        _eventBus = eventBus;
        _configuration = configuration;

        _logger = logger;
        _serviceFactory = serviceFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {



        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

        var factory = new ConnectionFactory
        {
            HostName = _configuration.GetSection("RabbitMQ")["HostName"] ?? "localhost",
            UserName = _configuration.GetSection("RabbitMQ")["UserName"] ?? "guest",
            Password = _configuration.GetSection("RabbitMQ")["Password"] ?? "guest"
        };
        _connection = await factory.CreateConnectionAsync(stoppingToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);


        var serviceScope = _serviceFactory.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<CoreModuleCtx>();

        await _channel.ExchangeDeclareAsync(Exchanges.UserTopicExchange, ExchangeType.Topic, true, false, null);

        await _channel.QueueDeclareAsync(
            queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        await _channel.QueueBindAsync(_queueName, Exchanges.UserTopicExchange, "user.registered", null);



        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                // نکته مهم نسخه ۷: حافظه ReadOnlyMemory متعلق به کتابخانه است، پس برای پردازش امن ابتدا کپی یا به استرینگ تبدیل می‌شود
                var userJson = Encoding.UTF8.GetString(ea.Body.ToArray());
                var user = JsonConvert.DeserializeObject<UserRegistered>(userJson);
                if (user != null)
                {
                    // ایجاد اسکوپ اختصاصی برای پردازش همین یک پیام
                    using (var scope = _serviceFactory.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<CoreModuleCtx>();

                        // بررسی Idempotency (جلوگیری از خطای درج کلید تکراری در دیتابیس)
                        var exists = await context.Users.AnyAsync(u => u.Id == user.Id, stoppingToken);
                        if (!exists)
                        {
                            context.Users.Add(new User
                            {
                                Id = user.Id,
                                CreationDate = user.CreationDate,
                                PhoneNumber = user.PhoneNumber,
                                Name = user.Name,
                                Family = user.Family,
                                Email = user.Email
                            });

                            await context.SaveChangesAsync(stoppingToken);
                            _logger.LogInformation("کاربر جدید با موفقیت در CoreModule ثبت شد: {UserId}", user.Id);
                        }
                        else
                        {
                            _logger.LogWarning("کاربر با شناسه {UserId} قبلاً ثبت شده است (پیام تکراری)", user.Id);
                        }
                    }
                }


                await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در پردازش پیام از صف {QueueName}", _queueName);
                await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        await _channel.BasicConsumeAsync(
            queue: _queueName,
            autoAck: false, 
            consumer: consumer,
            cancellationToken: stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);

    }
}