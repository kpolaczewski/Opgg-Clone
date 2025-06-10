using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class MatchHistoryModel : PageModel
{
    public string AccountName { get; set; }
    public string TagLine { get; set; }
    public int ProfileIconId { get; set; }
    public long SummonerLevel { get; set; }
    public string Puuid { get; set; }
    public List<MatchViewModel> Matches { get; set; } = new();

    public async Task OnGetAsync(string accountName, string tagLine)
    {
        AccountName = accountName;
        TagLine = tagLine;

        using var client = new HttpClient();
        // Pobierz dane konta i summoner
        var accountResp = await client.GetFromJsonAsync<AccountResponse>($"https://localhost:7179/api/riot/account/{accountName}/{tagLine}");
        if (accountResp != null)
        {
            ProfileIconId = accountResp.ProfileIconId;
            SummonerLevel = accountResp.SummonerLevel;
            Puuid = accountResp.Puuid;

            // Pobierz mecze
            var matchesResp = await client.GetFromJsonAsync<MatchesResponse>($"https://localhost:7179/api/riot/refresh-matches/{Puuid}");
            if (matchesResp != null && matchesResp.Matches != null)
            {
                Matches = matchesResp.Matches;
            }
        }
    }

    public class AccountResponse
    {
        public string Puuid { get; set; }
        public string SummonerId { get; set; }
        public long SummonerLevel { get; set; }
        public int ProfileIconId { get; set; }
    }

    public class MatchViewModel
    {
        public string ChampionName { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public bool Win { get; set; }
        public long GameDuration { get; set; }
        public long GameCreation { get; set; }
        public string GameMode { get; set; }
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

    public class MatchesResponse
    {
        public int NewMatchCount { get; set; }
        public List<MatchViewModel> Matches { get; set; }
    }
}