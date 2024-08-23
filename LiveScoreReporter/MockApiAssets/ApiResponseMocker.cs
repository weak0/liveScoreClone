using LiveScoreReporter.Application.Models;
using Newtonsoft.Json;

namespace LiveScoreReporter.MockApiAssets
{
    public class ApiResponseMocker
    {
        private readonly string _jsonFilePath;

        public ApiResponseMocker()
        {
            _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "MockApiAssets", "fixture_1202951.json");
        }
        
        public async Task<Root> ReturnFixtureObject()
        {
            string jsonResponse = await File.ReadAllTextAsync(_jsonFilePath);
           
            var result = JsonConvert.DeserializeObject<Root>(jsonResponse);

            return result;
        }
    }
}
