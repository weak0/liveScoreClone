using LiveScoreReporter.EFCore.Infrastructure;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LiveScoreReporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrontendController : ControllerBase
    {
        private readonly IGenericRepository<Game> _gameRepository;
        private readonly LiveScoreReporterDbContext _context;

        public FrontendController(IGenericRepository<Game> gameRepository, LiveScoreReporterDbContext context)
        {
            _gameRepository = gameRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGamesForLandingPageAsync()
        {
            var gamesWithScoresAndTeams = _context.Games
                .Include(g => g.Score)
                .Include(g => g.AwayTeam)
                .Include(g => g.HomeTeam);

            var frontDto = gamesWithScoresAndTeams.Select(x => new
            {
                GameId = x.FixtureId,
                HomeTeamName = x.HomeTeam.Name,
                HomeTeamLogo = x.HomeTeam.Logo,
                AwayTeamName = x.AwayTeam.Name,
                AwayTeamLogo = x.AwayTeam.Logo,
                GameResult = $"{x.Score.Home}:{x.Score.Away}"
            }).ToList();

            var dtoInJson = JsonConvert.SerializeObject(frontDto);

            return Ok(dtoInJson);
        }
    }
}
