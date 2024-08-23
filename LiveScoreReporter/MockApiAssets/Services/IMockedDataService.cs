using LiveScoreReporter.Application.Models;

namespace LiveScoreReporter.MockApiAssets.Services
{
    public interface IMockedDataService
    {
        Task<Root> GetMockedMatchDetailsAsync();
    }
}
