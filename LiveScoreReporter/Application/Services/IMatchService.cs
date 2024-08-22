using Newtonsoft.Json.Linq;
using System.Text.Json;
using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Controllers;

namespace LiveScoreReporter.Application.Services
{
    public interface IMatchService
    {
        Task<Root> GetMatchDetailsAsync(int fixtureId);
    }
}
