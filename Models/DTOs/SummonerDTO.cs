using Newtonsoft.Json;

namespace RiotStatsAPI.Models.DTOs
{
    public class SummonerDto
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        [JsonProperty("profileIconId")]
        public int ProfileIconId { get; set; }
        [JsonProperty("revisionDate")]
        public long RevisionDate { get; set; }
        [JsonProperty("id")]
        public string SummonerId { get; set; }
        [JsonProperty("puuid")]
        public string Puuid { get; set; }
        [JsonProperty("summonerLevel")]
        public long SummonerLevel { get; set; }
    }
}
