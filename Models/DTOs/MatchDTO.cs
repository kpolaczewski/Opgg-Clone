using Newtonsoft.Json;

namespace RiotStatsAPI.Models.DTOs
{
    public class MatchDTO
    {
        [JsonProperty("metadata")]
        public MetaDataDTO MetaDataDto { get; set; }

        [JsonProperty("info")]
        public InfoDTO InfoDto { get; set; }
    }
}
