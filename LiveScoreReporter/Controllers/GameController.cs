using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveScoreReporter.Controllers;

[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly IEventService _eventService;

    public GameController(IGameService gameService, IEventService eventService)
    {
        _gameService = gameService;
        _eventService = eventService;
    }
        
    [HttpGet]
    [Route("/games")]
    public async Task<ActionResult<List<GameDto>>> GetAllGamesForLandingPageAsync()
    {
        var gamesWithScoresAndTeams = await _gameService.GetGamesWithDetailsAsync();

        var gamesWithDetailsDto =  _gameService.MapGamesToDto(gamesWithScoresAndTeams);
        return Ok(gamesWithDetailsDto);
    }

    [HttpGet]
    [Route("/games/{gameId}")]
    public async Task<ActionResult<GameDto>> GetGameDetails([FromRoute] int gameId)
    {
        var gamesWithScoresAndTeams = await _gameService.GetSingleGameWithDetailsAsync(gameId);
        
        if (gamesWithScoresAndTeams == null)
        {
            return NotFound();
        }
        
        var gameLineup = await _gameService.GetGameLineupAsync(gameId);

        if (gameLineup.Count != 2)
        {
            return NotFound("Lineup not found");
        }
        
        var gameEvents = await _eventService.GetGameEventsWithDetailsAsync(gameId);
        
        if (gameEvents.Count == 0)
        {
            return NotFound("Events not found");
        }
        
        var gamesWithDetailsDto = _gameService.MapToGameDetailsDto(gamesWithScoresAndTeams, gameLineup[0], gameLineup[1], gameEvents);

        return Ok(gamesWithDetailsDto);
    }

}