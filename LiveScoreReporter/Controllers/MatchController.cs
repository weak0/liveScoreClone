using System.Text;
using System.Threading.Channels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace LiveScoreReporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly RabbitMQSettings _rabbitMQSettings;

        public MatchController(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;
           
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672"),
                ClientProvidedName = "Match Controller Sender"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_rabbitMQSettings.ExchangeName, ExchangeType.Direct, true);
            _channel.QueueDeclare(_rabbitMQSettings.QueueName, true, false, false, null);
            _channel.QueueBind(_rabbitMQSettings.QueueName, _rabbitMQSettings.ExchangeName, _rabbitMQSettings.RoutingKey, null);
        }

        [HttpPost("event")]
        public IActionResult SendMatchEvent([FromBody] string matchEvent)
        {
            var body = Encoding.UTF8.GetBytes(matchEvent);

            _channel.BasicPublish(_rabbitMQSettings.ExchangeName, _rabbitMQSettings.RoutingKey, null, body);

            return Ok();
        }
        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
