using LiveScoreReporter.Application.Models;
using LiveScoreReporter.Application.Services.Interfaces;
using LiveScoreReporter.EFCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace LiveScoreReporter.Seeder;


public class DbSeeder(ISeederService seederService, LiveScoreReporterDbContext dbContext)
{
    public async Task SeedAsync()
    {
        
        await dbContext.Database.MigrateAsync();
        
        if (!dbContext.Games.Any())
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(),"Infrastructure", "Seeder", "Data", "Games.json");
            var jsonData = await File.ReadAllTextAsync(path);
            var deserializedData = JsonConvert.DeserializeObject<Root>(jsonData);
                if (deserializedData != null)
                    await seederService.AddDataToDb(deserializedData);
        }
        
        await dbContext.SaveChangesAsync();
    }
}