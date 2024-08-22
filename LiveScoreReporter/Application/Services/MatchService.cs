using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Controllers;
using Newtonsoft.Json;
using RestSharp;

namespace LiveScoreReporter.Application.Services
{
    public class MatchService : IMatchService
    {
        private readonly RestClient _restClient;

        public MatchService()
        {
            _restClient = new RestClient("https://v3.football.api-sports.io/");
        }

        public async Task<Root> GetMatchDetailsAsync(int fixtureId)
        {
            try
            {
                var request = new RestRequest($"fixtures?id={fixtureId}", Method.Get);
                request.AddHeader("x-apisports-key", "fab0ac27356d28803f179b48d220d297");

                var response = await _restClient.ExecuteAsync(request);

                if (!response.IsSuccessful)
                {
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {response.Content}");
                }

                var test = JsonConvert.DeserializeObject<Root>(response.Content);

                return test;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
