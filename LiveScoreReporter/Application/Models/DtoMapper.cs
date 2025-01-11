using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.Application.Services;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using Entities = LiveScoreReporter.EFCore.Infrastructure.Entities;




namespace LiveScoreReporter.Application.Models;
public interface IDtoMapper
{
    PlayerDetailsDto MapPlayerDetails(Entities.Player player);
    
    PlayerDto MapPlayerToDto(Entities.Player player);
    TeamDto MapTeamToDto(Entities.Team team);
    TeamDetailsDto MapTeamDetails(Entities.Team team, ICollection<Game> games);
}

public class DtoMapper(IGameService gameService, 
    IEventService eventService, 
    IPlayerService playerService, 
    ITeamService teamService) : IDtoMapper
{
    
    public PlayerDto MapPlayerToDto(Entities.Player player) => new (player.Id, player.Name, player.Postition, player.Photo);
    
    public PlayerDetailsDto MapPlayerDetails(Entities.Player  player) => new (
        player.Id,
        player.Name, 
        player.Postition, 
        player.Photo, eventService.MapEventsToDto(player.Events.ToList()),
        MapTeamToDto(player.Lineups.First().Team)
    );
    
    public TeamDto MapTeamToDto(Entities.Team team) => new (team.Id, team.Name, team.Logo);
    
    public TeamDetailsDto MapTeamDetails(Entities.Team team, ICollection<Game> games) => new (
        team.Id,
        team.Name,
        team.Logo,
        team.Lineups
            .SelectMany(l => l.Players)
            .Select(p => MapPlayerToDto(p))
            .ToList(),
        games.Select(g => gameService.MapSingleGameToDto(g)).ToList()
    );
}