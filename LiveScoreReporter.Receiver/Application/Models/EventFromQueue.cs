using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreReporter.Receiver.Application.Models
{
    public class Assist
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }

    public class Player
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class EventFromQueue
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
        public object? Comments { get; set; }
        [JsonProperty("fixtureId")]
        public int FixtureId { get; set; }
    }

    public class Team
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }
    }

    public class Time
    {
        [JsonProperty("elapsed")]
        public int Elapsed { get; set; }

        [JsonProperty("extra")]
        public object? Extra { get; set; }
    }
}
