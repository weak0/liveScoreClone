using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveScoreReporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        
        [HttpGet]
        [Route("/games")]
        public async Task<ActionResult<List<GameWithDetailsDto>>> GetAllGamesForLandingPageAsync()
        {
            var gamesWithScoresAndTeams = await _gameService.GetGamesWithDetailsAsync();

            var gamesWithDetailsDto =  _gameService.MapGamesToDto(gamesWithScoresAndTeams);
            return Ok(gamesWithDetailsDto);
        }

        [HttpGet]
        [Route("/games/{gameId}")]
        public async Task<ActionResult<GameWithDetailsDto>> GetGameDetails([FromRoute] int gameId)
        {
            var gamesWithScoresAndTeams = await _gameService.GetSingleGameWithDetailsAsync(gameId);

            var gamesWithDetailsDto = _gameService.MapSingleGameToDto(gamesWithScoresAndTeams);

            return Ok(gamesWithDetailsDto);
        }

    }
}
