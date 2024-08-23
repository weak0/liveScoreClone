using LiveScoreReporter.Application.Models;
using LiveScoreReporter.MockApiAssets;

namespace LiveScoreReporter.MockApiAssets.Services
{
    public class MockedDataService : IMockedDataService
    {
        public async Task<Root> GetMockedMatchDetailsAsync()
        {
            try
            {
                var mock = new ApiResponseMocker();

                return await mock.ReturnFixtureObject();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
