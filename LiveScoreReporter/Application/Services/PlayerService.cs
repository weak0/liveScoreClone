using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure.Repositories.Interfaces;

namespace LiveScoreReporter.Application.Services;

public class PlayerService(IPlayerRepository playerRepository) : IPlayerService
{
    
}