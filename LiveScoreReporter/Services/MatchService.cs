using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LiveScoreReporter.Services
{
    public class MatchService 
    {
        private readonly HttpClient _httpClient;

        public MatchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JObject> GetMatchDetailsAsync(int fixtureId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://v3.football.api-sports.io/fixtures?id={fixtureId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "fab0ac27356d28803f179b48d220d297");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JObject.Parse(content);
        }
    }
}
