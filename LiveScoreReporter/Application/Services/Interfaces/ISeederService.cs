using Newtonsoft.Json.Linq;
using System.Text.Json;
using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Controllers;
using System.Threading.Tasks;

namespace LiveScoreReporter.Application.Services.Interfaces
{
    public interface ISeederService
    {
        Task<Root> GetMatchDetailsAsync(int fixtureId);
        Task<bool> AddDataToDb(Root obj);
        Task SeedGameLineupAsync(int fixtureId);
        Task AddOrUpdatePlayersAsync(IEnumerable<Lineup> lineups);
        Task SeedGameEventsAsync(int gameId);
    }
}
