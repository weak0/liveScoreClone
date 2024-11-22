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
        [Route("/games/all")]
        public async Task<IActionResult> GetAllGamesForLandingPageAsync()
        {
            var gamesWithScoresAndTeams = await _gameService.GetGamesWithDetailsAsync();

            var gamesWithDetailsDtos =  _gameService.MapGamesToDto(gamesWithScoresAndTeams);

            var dtosSerializedToJson = _gameService.SerializeGamesToJson(gamesWithDetailsDtos);

            return Ok(dtosSerializedToJson);
        }

        [HttpGet]
        [Route("/games/{gameId}")]
        public async Task<IActionResult> GetGameDetails([FromRoute] int gameId)
        {
            var gamesWithScoresAndTeams = await _gameService.GetSingleGameWithDetailsAsync(gameId);

            var gamesWithDetailsDtos = _gameService.MapSingleGameToDto(gamesWithScoresAndTeams);

            var dtosSerializedToJson = _gameService.SerializeSingleGameToJson(gamesWithDetailsDtos);

            return Ok(dtosSerializedToJson);
        }

    }
}
