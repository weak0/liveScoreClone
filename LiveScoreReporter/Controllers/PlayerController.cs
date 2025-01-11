using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LiveScoreReporter.Controllers;

[ApiController]
[Route("/players")]
public class PlayerController(IPlayerService playerService, IEventService eventService, IDtoMapper dtoMapper) : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<ICollection<PlayerDto>>> GetPlayers()
    {
        var players = await playerService.GetPlayersAsync();

        if (players == null)
        {
            return NotFound();
        }

        return Ok(players.Select(player => dtoMapper.MapPlayerToDto(player)));
    }
    
    [HttpGet("{playerId}")]
    public async Task<ActionResult<PlayerDetailsDto>> GetPlayerDetails([FromRoute] int playerId)
    {
        var player = await playerService.GetPlayerAsync(playerId);

        if (player == null)
        {
            return NotFound();
        }

        return Ok(dtoMapper.MapPlayerDetails(player));
    }




}