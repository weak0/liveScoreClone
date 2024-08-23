namespace LiveScoreReporter.Sender.RabbitMq.Settings
{
    public class RabbitMqSettings
    {
        public string Uri { get; set; }
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
        public string QueueName { get; set; }
    }
}
