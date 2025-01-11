using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LiveScoreReporter.Controllers;

[ApiController]
[Route("/players")]
public class PlayerController(IPlayerService playerService, IEventService eventService) : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<ICollection<PlayerDto>>> GetPlayers()
    {
        var players = await playerService.GetPlayersAsync();

        if (players == null)
        {
            return NotFound();
        }

        return Ok(players.Select(MapPlayerToDto));
    }
    
    [HttpGet("{playerId}")]
    public async Task<ActionResult<PlayerDetailsDto>> GetPlayerDetails([FromRoute] int playerId)
    {
        var player = await playerService.GetPlayerAsync(playerId);

        if (player == null)
        {
            return NotFound();
        }

        return Ok(MapPlayerDetails(player));
    }

    private static PlayerDto MapPlayerToDto(Player player) => new (player.Id, player.Name, player.Postition, player.Photo);
    private PlayerDetailsDto MapPlayerDetails(Player player) => new (player.Id, player.Name, player.Postition, 
        player.Photo, eventService.MapEventsToDto(player.Events.ToList()));


}