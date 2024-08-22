namespace LiveScoreReporter.EFCore.Infrastructure.Entities
{
    public class League
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public string Country { get; set; }
        public string? Logo { get; set; }
        public string? Flag { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
