using Newtonsoft.Json;

namespace RiotStatsAPI.Models.DTOs
{
    public class AccountDTO
    {
        [JsonProperty("puuid")]
        public required string Puuid { get; set; }
        [JsonProperty("gameName")]
        public string? GameName { get; set; }
        [JsonProperty("tagLine")]
        public string? tagLine { get; set; }
    }
}
