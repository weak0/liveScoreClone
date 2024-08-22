namespace LiveScoreReporter.EFCore.Infrastructure.Entities
{
    public class Score
    {
        public int Id { get; set; }
    
        public int Home { get; set; }
        public int Away{ get; set; }
        public string? Result{ get; set; }
    }
}
