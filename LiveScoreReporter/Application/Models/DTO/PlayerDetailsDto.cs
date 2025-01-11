namespace LiveScoreReporter.Application.Models.DTO;

public record PlayerDetailsDto(int? Id, string Name, string Position, string Photo, ICollection<EventWithDetailsDto> Events);