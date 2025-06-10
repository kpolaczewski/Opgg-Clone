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

        //PRZEDMIOTY
        [JsonProperty("item0")]
        public int Item0 { get; set; }

        [JsonProperty("item1")]
        public int Item1 { get; set; }

        [JsonProperty("item2")]
        public int Item2 { get; set; }

        [JsonProperty("item3")]
        public int Item3 { get; set; }

        [JsonProperty("item4")]
        public int Item4 { get; set; }

        [JsonProperty("item5")]
        public int Item5 { get; set; }

        [JsonProperty("item6")]
        public int Item6 { get; set; }

        [JsonProperty("teamId")]
        public int TeamId { get; set; }
    }
}