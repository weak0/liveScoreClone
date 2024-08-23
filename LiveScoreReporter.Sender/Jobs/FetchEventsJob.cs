using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            request.AddHeader("x-apisports-key", "fab0ac27356d28803f179b48d220d297");

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = response.Content;

                // Parsowanie JSON do obiektu JObject
                var json = JObject.Parse(content);

                // Iterowanie po eventach w odpowiedzi
                foreach (var eventItem in json["response"])
                {
                    // Konwertowanie pojedynczego eventu do JSON stringa
                    var eventJson = eventItem.ToString();

                    // Publikowanie każdego eventu do kolejki
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
