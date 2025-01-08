namespace LiveScoreReporter.EFCore.Infrastructure.Entities
{
    public class Game
    {
        public int FixtureId { get; set; }
    
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int? ScoreId { get; set; }
        public int LeagueId { get; set; }


        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Score Score { get; set; }
        public League League { get; set; }

        public ICollection<Event> Events { get; set; }
        public ICollection<Lineup> Lineups { get; set; } 
    }
}
