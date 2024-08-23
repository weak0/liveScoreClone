using Newtonsoft.Json.Linq;
using System.Text.Json;
using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Controllers;
using System.Threading.Tasks;

namespace LiveScoreReporter.Application.Services
{
    public interface IMatchService
    {
        Task<Root> GetMatchDetailsAsync(int fixtureId);
        Task<bool> AddDataToDb(Root obj);
    }
}
