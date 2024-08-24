using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveScoreReporter.Shared.RabbitMq.Settings;

namespace LiveScoreReporter.Receiver
{
    public class ReceiverService : BackgroundService
    {
        private readonly ILogger<ReceiverService> _logger;
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly RabbitMqSettings _rabbitMQSettings;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ReceiverService(ILogger<ReceiverService> logger, IOptions<RabbitMqSettings> rabbitMQSettings, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _rabbitMQSettings = rabbitMQSettings.Value;

            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_rabbitMQSettings.Uri)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(_rabbitMQSettings.QueueName, true, false, false, null);
            _channel.QueueBind(_rabbitMQSettings.QueueName, _rabbitMQSettings.ExchangeName, _rabbitMQSettings.RoutingKey, null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ReceiverService started.");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    // Tworzenie nowego zakresu dla każdego przetwarzanego eventu
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var eventProcessor = scope.ServiceProvider.GetRequiredService<IEventProcessor>();
                        await eventProcessor.ProcessEventAsync(message);
                    }

                    // Potwierdzenie przetworzenia wiadomości
                    _channel.BasicAck(ea.DeliveryTag, false);
                    _logger.LogInformation("Message processed and acknowledged.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message, will not acknowledge.");

                    _channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            _channel.BasicConsume(queue: _rabbitMQSettings.QueueName, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
