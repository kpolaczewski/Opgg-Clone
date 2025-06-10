using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiotStatsAPI.Models.Entities
{
    public class Participant
    {
        public long Id { get; set; }

        public string ChampionName { get; set; }
        public int Assists { get; set; }
        public int Deaths { get; set; }
        public int Kills { get; set; }
        public string Puuid { get; set; }
        public string RiotIdGameName { get; set; }
        public bool Win { get; set; }
        public int ChampionLevel { get; set; }
        public int Item0 { get; set; }
        public int Item1 { get; set; }
        public int Item2 { get; set; }
        public int Item3 { get; set; }
        public int Item4 { get; set; }
        public int Item5 { get; set; }
        public int Item6 { get; set; }
        public int TeamId { get; set; }
        public string GameId { get; set; }
    }
}