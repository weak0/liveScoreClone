using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveScoreReporter.Shared.RabbitMq.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace LiveScoreReporter.Sender.RabbitMq
{
    public class RabbitMqProducer : IQueueProducer, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly RabbitMqSettings _rabbitMQSettings;

        public RabbitMqProducer(IOptions<RabbitMqSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;

            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_rabbitMQSettings.Uri),
                ClientProvidedName = "Event Sender"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_rabbitMQSettings.ExchangeName, ExchangeType.Direct, true);
            _channel.QueueDeclare(_rabbitMQSettings.QueueName, true, false, false, null);
            _channel.QueueBind(_rabbitMQSettings.QueueName, _rabbitMQSettings.ExchangeName, _rabbitMQSettings.RoutingKey, null);
        }

        public Task PublishAsync(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: _rabbitMQSettings.ExchangeName,
                routingKey: _rabbitMQSettings.RoutingKey,
                basicProperties: null,
                body: body);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
