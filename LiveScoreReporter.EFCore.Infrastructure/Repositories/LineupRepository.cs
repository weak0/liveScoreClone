using LiveScoreReporter.EFCore.Infrastructure.Entities;

namespace LiveScoreReporter.EFCore.Infrastructure.Repositories;

public interface ILineupRepository
{
    public Task AddOrUpdateLineupAsync(Lineup lineup);
}

public class LineupRepository : ILineupRepository
{
    private readonly LiveScoreReporterDbContext _dbContext;

    public LineupRepository(LiveScoreReporterDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddOrUpdateLineupAsync(Lineup lineup)
    {
        var existingLineup = await _dbContext.Lineups.FindAsync(lineup.Id);
        if (existingLineup == null)
        {
            await _dbContext.Lineups.AddAsync(lineup);
        }
        else
        {
            _dbContext.Entry(existingLineup).CurrentValues.SetValues(lineup);
        }

        await _dbContext.SaveChangesAsync();
    }
    
}

