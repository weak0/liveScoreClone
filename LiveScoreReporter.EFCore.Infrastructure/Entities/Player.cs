
namespace LiveScoreReporter.EFCore.Infrastructure.Entities
{
    public class Player
    {
        public int? Id { get; set; }
       
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
        public ICollection<Event> AssistedEvents { get; set; }
        public ICollection<Lineup> Lineups { get; set; }
    }
}
