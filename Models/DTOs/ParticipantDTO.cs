using Newtonsoft.Json;

namespace RiotApiStats.Models.DTOs
{
    public class ParticipantDTO
    {
        [JsonProperty("championName")]
        public string ChampionName { get; set; }

        [JsonProperty("riotIdGameName")]
        public string RiotIdGameName { get; set; }

        [JsonProperty("assists")]
        public int Assists { get; set; }

        [JsonProperty("deaths")]
        public int Deaths { get; set; }

        [JsonProperty("kills")]
        public int Kills { get; set; }

        [JsonProperty("puuid")]
        public string Puuid { get; set; }

        [JsonProperty("win")]
        public bool Win { get; set; }

        [JsonProperty("champLevel")]
        public int ChampionLevel { get; set; }
        
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
