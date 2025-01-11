using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveScoreReporter.Controllers;

[ApiController]
[Route("/teams")]
public class TeamController(ITeamService teamService, IDtoMapper dtoMapper) : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<ICollection<TeamDto>>> GetTeams()
    {
        var teams = await teamService.GetTeamsAsync();

        if (teams == null)
        {
            return NotFound();
        }

        return Ok(teams.Select(t => dtoMapper.MapTeamToDto(t)).ToList());
    }
    
    [HttpGet("{teamId}")]
    public async Task<ActionResult<TeamDetailsDto>> GetTeamDetails([FromRoute] int teamId)
    {
        var team = await teamService.GetTeamAsync(teamId);
        
        if (team == null)
        {
            return NotFound();
        }
        
        var games = await teamService.GetTeamGames(teamId);
        

        return Ok(dtoMapper.MapTeamDetails(team, games));
    }
    
}