using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace LiveScoreReporter.Seeder;


public class DbSeeder(IMatchService matchService, LiveScoreReporterDbContext dbContext)
{
    public async Task SeedAsync()
    {
        
        await dbContext.Database.MigrateAsync();
        
        if (!dbContext.Games.Any())
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Seeder", "Data", "Games.json");
            var jsonData = await File.ReadAllTextAsync(path);
            var deserializedData = JsonConvert.DeserializeObject<Root>(jsonData);
                if (deserializedData != null)
                    await matchService.AddDataToDb(deserializedData);
        }

        if (!dbContext.Players.Any())
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Seeder", "Data", "Lineups.json");
            var jsonData = await File.ReadAllTextAsync(path);
            var deserializedData = JsonConvert.DeserializeObject<LineupResponse>(jsonData);
            if (deserializedData != null)
                await matchService.AddOrUpdatePlayersAsync(deserializedData.Response);
        }
        
        await dbContext.SaveChangesAsync();
        
    }
}