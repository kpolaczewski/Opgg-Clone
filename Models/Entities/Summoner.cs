using Newtonsoft.Json;

namespace RiotStatsAPI.Models.Entities
{
    public class Summoner
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public Account Account { get; set; }

        [JsonProperty("encryptedAccountId")]
        public string EncryptedAccountId { get; set; }
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
