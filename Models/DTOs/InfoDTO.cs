using Newtonsoft.Json;
using RiotApiStats.Models.DTOs;

namespace RiotStatsAPI.Models.DTOs
{
    public class InfoDTO
    {
        [JsonProperty("endOfGameResult")]
        public string EndOfGameResult { get; set; }
        [JsonProperty("gameCreation")]
        public long GameCreation { get; set; }
        [JsonProperty("gameDuration")]
        public long GameDuration { get; set; }
        [JsonProperty("gameEndTimestamp")]
        public long GameEndTimestamp { get; set; }
        [JsonProperty("gameId")]
        public long GameId { get; set; }
        [JsonProperty("gameMode")]
        public string GameMode { get; set; }
        [JsonProperty("gameName")]
        public string GameName { get; set; }
        [JsonProperty("gameStartTimestamp")]
        public long GameStartTimestamp { get; set; }
        [JsonProperty("gameType")]
        public string GameType { get; set; }
        [JsonProperty("gameVersion")]
        public string GameVersion { get; set; }
        [JsonProperty("mapId")]
        public int MapId { get; set; }
        [JsonProperty("participants")]
        public List<ParticipantDTO> Participants { get; set; }
        [JsonProperty("platformId")]
        public string PlatformId { get; set; }
        [JsonProperty("queueId")]
        public string QueueId { get; set; }
        
    }
}
