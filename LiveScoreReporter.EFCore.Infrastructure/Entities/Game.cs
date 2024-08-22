namespace LiveScoreReporter.EFCore.Infrastructure.Entities
{
    public class Game
    {
        public int FixtureId { get; set; }
    
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int ScoreId { get; set; }
        public int LeagueId { get; set; }
    }
}
