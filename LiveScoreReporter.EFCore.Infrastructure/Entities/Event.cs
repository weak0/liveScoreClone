namespace LiveScoreReporter.EFCore.Infrastructure.Entities
{
    public class Event
    {
        public int Id { get; set; }
       
        public int GameId { get; set; }
        public int TeamId { get; set; }
        public string Type { get; set; }
        public string Details{ get; set; }
        public string Comments { get; set; }
        public int Time { get; set; }
        public int PlayerId { get; set; }
        public int? AssistPlayerId { get; set; }
    }
}
