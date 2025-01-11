namespace LiveScoreReporter.Application.Models.DTO;

public record TeamDetailsDto(int TeamId, string TeamName, string TeamLogo, ICollection<PlayerDto> Players, ICollection<GameDto> games);