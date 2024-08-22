namespace LiveScoreReporter.EFCore.Infrastructure.Entities
{
    public class Team
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
        public string? Logo { get; set; }

        public ICollection<Game> HomeGames { get; set; }
        public ICollection<Game> AwayGames { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
