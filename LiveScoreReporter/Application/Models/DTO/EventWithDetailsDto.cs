using LiveScoreReporter.EFCore.Infrastructure.Entities;

namespace LiveScoreReporter.Application.Models.DTO
{
    public record EventWithDetailsDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public EventType Type { get; set; }
        public string Details { get; set; }
        public string? Comments { get; set; }
        public int? Time { get; set; }
        public string PlayerName { get; set; }
        public string? AssistPlayerName { get; set; }
    }
}
