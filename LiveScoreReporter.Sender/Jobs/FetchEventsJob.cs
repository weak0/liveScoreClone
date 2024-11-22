using LiveScoreReporter.Sender.RabbitMq;
using Newtonsoft.Json.Linq;
using Quartz;
using RestSharp;

namespace LiveScoreReporter.Sender.Jobs
{
    public class FetchEventsJob : IJob
    {
        private readonly ILogger<FetchEventsJob> _logger;
        private readonly IQueueProducer _queueProducer;
        private readonly RestClient _client;

        public FetchEventsJob(ILogger<FetchEventsJob> logger, IQueueProducer queueProducer, RestClient client)
        {
            _logger = logger;
            _queueProducer = queueProducer;
            _client = new RestClient("https://v3.football.api-sports.io/");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var fixtureId = context.MergedJobDataMap.GetInt("fixtureId");

            _logger.LogInformation("Fetching events for fixture {fixtureId} at {time}", fixtureId, DateTimeOffset.Now);

            var request = new RestRequest($"fixtures/events?fixture={fixtureId}", Method.Get);
            request.AddHeader("x-apisports-key", "4038020fe2c0d80536856c4f340a1732");

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = response.Content;
               
                var json = JObject.Parse(content);

                foreach (var eventItem in json["response"])
                {
                    eventItem["fixtureId"] = fixtureId;

                    var eventJson = eventItem.ToString();

                    await _queueProducer.PublishAsync(eventJson);

                    _logger.LogInformation("Published event for fixture {fixtureId} to queue at {time}", fixtureId, DateTimeOffset.Now);
                }
            }
            else
            {
                _logger.LogError("Failed to fetch events for fixture {fixtureId}: {statusCode}", fixtureId, response.StatusCode);
            }
        }
    }
}
