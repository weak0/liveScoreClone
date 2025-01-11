using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.EFCore.Infrastructure.Entities;

namespace LiveScoreReporter.Application.Services;

public interface ITeamService
{
    Task<Team?> GetTeamAsync(int teamId);
    Task<ICollection<Team>> GetTeamsAsync();
    Task<ICollection<Game>> GetTeamGames(int teamId);
}