using Newtonsoft.Json;

namespace LiveScoreReporter.Application.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Assist
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Away
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("winner")]
        public bool Winner { get; set; }
    }

    public class Cards
    {
        [JsonProperty("yellow")]
        public int Yellow { get; set; }

        [JsonProperty("red")]
        public int Red { get; set; }
    }

    public class Coach
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }
    }

    public class Colors
    {
        [JsonProperty("player")]
        public Player Player { get; set; }

        [JsonProperty("goalkeeper")]
        public Goalkeeper Goalkeeper { get; set; }
    }

    public class Dribbles
    {
        [JsonProperty("attempts")]
        public int? Attempts { get; set; }

        [JsonProperty("success")]
        public int? Success { get; set; }

        [JsonProperty("past")]
        public int? Past { get; set; }
    }

    public class Duels
    {
        [JsonProperty("total")]
        public int? Total { get; set; }

        [JsonProperty("won")]
        public int? Won { get; set; }
    }

    public class Event
    {
        [JsonProperty("time")]
        public Time Time { get; set; }

        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("player")]
        public Player Player { get; set; }

        [JsonProperty("assist")]
        public Assist Assist { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }
    }

    public class Extratime
    {
        [JsonProperty("home")]
        public object Home { get; set; }

        [JsonProperty("away")]
        public object Away { get; set; }
    }

    public class Fixture
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("referee")]
        public string Referee { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty("periods")]
        public Periods Periods { get; set; }

        [JsonProperty("venue")]
        public Venue Venue { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }
    }

    public class Fouls
    {
        [JsonProperty("drawn")]
        public int? Drawn { get; set; }

        [JsonProperty("committed")]
        public int? Committed { get; set; }
    }

    public class Fulltime
    {
        [JsonProperty("home")]
        public int Home { get; set; }

        [JsonProperty("away")]
        public int Away { get; set; }
    }

    public class Games
    {
        [JsonProperty("minutes")]
        public int? Minutes { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("captain")]
        public bool Captain { get; set; }

        [JsonProperty("substitute")]
        public bool Substitute { get; set; }
    }

    public class Goalkeeper
    {
        [JsonProperty("primary")]
        public string Primary { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("border")]
        public string Border { get; set; }
    }

    public class Goals
    {
        [JsonProperty("home")]
        public int Home { get; set; }

        [JsonProperty("away")]
        public int Away { get; set; }

        [JsonProperty("total")]
        public int? Total { get; set; }

        [JsonProperty("conceded")]
        public int Conceded { get; set; }

        [JsonProperty("assists")]
        public int? Assists { get; set; }

        [JsonProperty("saves")]
        public int? Saves { get; set; }
    }

    public class Halftime
    {
        [JsonProperty("home")]
        public int Home { get; set; }

        [JsonProperty("away")]
        public int Away { get; set; }
    }

    public class Home
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("winner")]
        public bool Winner { get; set; }
    }

    public class League
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("season")]
        public int Season { get; set; }

        [JsonProperty("round")]
        public string Round { get; set; }
    }

    public class Lineup
    {
        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("coach")]
        public Coach Coach { get; set; }

        [JsonProperty("formation")]
        public string Formation { get; set; }

        [JsonProperty("startXI")]
        public List<StartXI> StartXI { get; set; }

        [JsonProperty("substitutes")]
        public List<Substitute> Substitutes { get; set; }
    }

    public class Paging
    {
        [JsonProperty("current")]
        public int Current { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class Parameters
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Passes
    {
        [JsonProperty("total")]
        public int? Total { get; set; }

        [JsonProperty("key")]
        public int? Key { get; set; }

        [JsonProperty("accuracy")]
        public string Accuracy { get; set; }
    }

    public class Penalty
    {
        [JsonProperty("home")]
        public object Home { get; set; }

        [JsonProperty("away")]
        public object Away { get; set; }

        [JsonProperty("won")]
        public object Won { get; set; }

        [JsonProperty("commited")]
        public object Commited { get; set; }

        [JsonProperty("scored")]
        public int Scored { get; set; }

        [JsonProperty("missed")]
        public int Missed { get; set; }

        [JsonProperty("saved")]
        public int? Saved { get; set; }
    }

    public class Periods
    {
        [JsonProperty("first")]
        public int First { get; set; }

        [JsonProperty("second")]
        public int Second { get; set; }
    }

    public class Player
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("primary")]
        public string Primary { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("border")]
        public string Border { get; set; }

        [JsonProperty("pos")]
        public string Pos { get; set; }

        [JsonProperty("grid")]
        public string Grid { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }
    }

    public class Player5
    {
        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("players")]
        public List<Player> Players { get; set; }

        [JsonProperty("player")]
        public Player Player { get; set; }

        [JsonProperty("statistics")]
        public List<Statistic> Statistics { get; set; }
    }

    public class Response
    {
        [JsonProperty("fixture")]
        public Fixture Fixture { get; set; }

        [JsonProperty("league")]
        public League League { get; set; }

        [JsonProperty("teams")]
        public Teams Teams { get; set; }

        [JsonProperty("goals")]
        public Goals Goals { get; set; }

        [JsonProperty("score")]
        public Score Score { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }

        [JsonProperty("lineups")]
        public List<Lineup> Lineups { get; set; }

        [JsonProperty("statistics")]
        public List<Statistic> Statistics { get; set; }

        [JsonProperty("players")]
        public List<Player> Players { get; set; }
    }

    public class Root
    {
        [JsonProperty("get")]
        public string Get { get; set; }

        [JsonProperty("parameters")]
        public Parameters Parameters { get; set; }

        [JsonProperty("errors")]
        public List<object> Errors { get; set; }

        [JsonProperty("results")]
        public int Results { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("response")]
        public List<Response> Response { get; set; }
    }

    public class Score
    {
        [JsonProperty("halftime")]
        public Halftime Halftime { get; set; }

        [JsonProperty("fulltime")]
        public Fulltime Fulltime { get; set; }

        [JsonProperty("extratime")]
        public Extratime Extratime { get; set; }

        [JsonProperty("penalty")]
        public Penalty Penalty { get; set; }
    }

    public class Shots
    {
        [JsonProperty("total")]
        public int? Total { get; set; }

        [JsonProperty("on")]
        public int? On { get; set; }
    }

    public class StartXI
    {
        [JsonProperty("player")]
        public Player Player { get; set; }
    }

    public class Statistic
    {
        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("statistics")]
        public List<Statistic> Statistics { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }

        [JsonProperty("games")]
        public Games Games { get; set; }

        [JsonProperty("offsides")]
        public int? Offsides { get; set; }

        [JsonProperty("shots")]
        public Shots Shots { get; set; }

        [JsonProperty("goals")]
        public Goals Goals { get; set; }

        [JsonProperty("passes")]
        public Passes Passes { get; set; }

        [JsonProperty("tackles")]
        public Tackles Tackles { get; set; }

        [JsonProperty("duels")]
        public Duels Duels { get; set; }

        [JsonProperty("dribbles")]
        public Dribbles Dribbles { get; set; }

        [JsonProperty("fouls")]
        public Fouls Fouls { get; set; }

        [JsonProperty("cards")]
        public Cards Cards { get; set; }

        [JsonProperty("penalty")]
        public Penalty Penalty { get; set; }
    }

    public class Status
    {
        [JsonProperty("long")]
        public string Long { get; set; }

        [JsonProperty("short")]
        public string Short { get; set; }

        [JsonProperty("elapsed")]
        public int Elapsed { get; set; }
    }

    public class Substitute
    {
        [JsonProperty("player")]
        public Player Player { get; set; }
    }

    public class Tackles
    {
        [JsonProperty("total")]
        public int? Total { get; set; }

        [JsonProperty("blocks")]
        public int? Blocks { get; set; }

        [JsonProperty("interceptions")]
        public int? Interceptions { get; set; }
    }

    public class Team
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("colors")]
        public Colors Colors { get; set; }

        [JsonProperty("update")]
        public DateTime Update { get; set; }
    }

    public class Teams
    {
        [JsonProperty("home")]
        public Home Home { get; set; }

        [JsonProperty("away")]
        public Away Away { get; set; }
    }

    public class Time
    {
        [JsonProperty("elapsed")]
        public int Elapsed { get; set; }

        [JsonProperty("extra")]
        public int? Extra { get; set; }
    }

    public class Venue
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }


}
