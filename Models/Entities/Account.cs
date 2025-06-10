using Newtonsoft.Json;

namespace RiotStatsAPI.Models.Entities
{
    public class Account
    {
        public long Id { get; set; }
        public DateTime? lastSynced { get; set; }

        [JsonProperty("puuid")]
        public string Puuid { get; set; }

        [JsonProperty("gameName")]
        public string? GameName { get; set; }

        [JsonProperty("tagLine")]
        public string? GameTag { get; set; }

        public Summoner? Summoner { get; set; }
    }
}
