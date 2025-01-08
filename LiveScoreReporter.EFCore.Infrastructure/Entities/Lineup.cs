namespace LiveScoreReporter.EFCore.Infrastructure.Entities;

public class Lineup
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int TeamId { get; set; }
    public ICollection<Player> Players { get; set; }
    public Team Team { get; set; }
    public Game Game { get; set; }
}