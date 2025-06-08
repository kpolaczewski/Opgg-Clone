using Newtonsoft.Json;

namespace RiotStatsAPI.Models.DTOs
{
    public class MetaDataDTO
    {
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }

        [JsonProperty("matchId")]
        public string MatchId { get; set; }

        [JsonProperty("participants")]
        public List<string> Participants { get; set; }
    }
}
