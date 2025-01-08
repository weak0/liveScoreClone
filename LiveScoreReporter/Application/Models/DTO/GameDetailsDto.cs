namespace LiveScoreReporter.Application.Models.DTO;

public class GameDetailsDto
{
    public int GameId { get; set; }
    public string HomeTeamName { get; set; }
    public string HomeTeamLogo { get; set; }
    public string AwayTeamName { get; set; }
    public string AwayTeamLogo { get; set; }
    public ICollection<string> HomeTeamLineup { get; set; }
    public ICollection<string> AwayTeamLineup { get; set; }
    public string GameResult { get; set; }
}