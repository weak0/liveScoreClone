using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure.Entities;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;

namespace LiveScoreReporter.Application.Services;

public class TeamService(ITeamRepository repository, IGameService gameService) : ITeamService
{
    public async Task<Team?> GetTeamAsync(int teamId)
    {
       return await repository.GetTeamAsync(teamId);
       
    }

    public async Task<ICollection<Team>> GetTeamsAsync()
    {
        return await repository.GetTeamsAsync();
    }
    public async Task<ICollection<Game>> GetTeamGames(int teamId)
    {
       return  (await gameService.GetGamesWithDetailsAsync())
           .Where(g => g.AwayTeam.Id == teamId|| g.HomeTeam.Id == teamId).ToList();
    }
}