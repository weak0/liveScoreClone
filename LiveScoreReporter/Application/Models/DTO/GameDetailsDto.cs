namespace LiveScoreReporter.Application.Models.DTO;

public class GameDetailsDto
{
    public int GameId { get; set; }
    public string HomeTeamName { get; set; }
    public string HomeTeamLogo { get; set; }
    public string AwayTeamName { get; set; }
    public string AwayTeamLogo { get; set; }
    public ICollection<PlayerDto> HomeTeamLineup { get; set; }
    public ICollection<PlayerDto> AwayTeamLineup { get; set; }
    public ICollection<EventWithDetailsDto> Events { get; set; }
    public string GameResult { get; set; }
}