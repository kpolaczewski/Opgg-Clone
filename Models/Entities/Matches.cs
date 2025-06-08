using Newtonsoft.Json;

namespace RiotStatsAPI.Models.Entities
{
    public class Matches
    {
        public long Id { get; set; }
        public long AccountId { get; set; }

        [JsonProperty("gameId")]
        public string GameId { get; set; }

        [JsonProperty("gameCreation")]
        public long GameCreation { get; set; }

        [JsonProperty("gameDuration")]
        public long GameDuration { get; set; }

        [JsonProperty("gameMode")]
        public string GameMode { get; set; }

        [JsonProperty("deaths")]
        public int Deaths { get; set; }

        [JsonProperty("kills")]
        public int Kills { get; set; }

        [JsonProperty("assists")]
        public int Assists { get; set; }

        [JsonProperty("championName")]
        public string ChampionName { get; set; }

        [JsonProperty("champLevel")]
        public int ChampionLevel { get; set; }

        [JsonProperty("win")]
        public bool Win { get; set; }

    }
}