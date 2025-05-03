using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RiotStatsAPI.Data;
using RiotStatsAPI.Models.DTOs;
using RiotStatsAPI.Models.Entities;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RiotStatsAPI.Services
{
    public class RiotApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly AppDbContext _dbContext;

        private void AppendQueryParam(ref string url, string paramName, string paramValue)
        {
            if (url.Contains('?'))
            {
                url += $"&{paramName}={paramValue}";
            }
            else
            {
                url += $"?{paramName}={paramValue}";
            }
        }

        public RiotApiService(HttpClient httpClient, string apiKey, AppDbContext dbContext)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _dbContext = dbContext;
        }

        public async Task<T> GetRiotApiData<T>(string url)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //_httpClient.DefaultRequestHeaders.Add("X-Riot-Token", _apiKey);
                AppendQueryParam(ref url, "api_key", _apiKey);

                // Send the request and get the response
                var response = await _httpClient.GetAsync(url);

                // Check if the response status code is successful
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Riot API responded with status code {response.StatusCode}: {response.ReasonPhrase}");
                }

                // Read the response content as a string
                var responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response to the desired object type
                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (Exception ex)
            {
                // Log the error or handle it as needed
                throw new Exception($"Error fetching data from Riot API: {ex.Message}");
            }
        }



        public async Task<Account> GetAccount(string gameName, string tagLine)
        { 
            string url = $"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}";
            var foundAccount = await GetRiotApiData<AccountDTO>(url); // Added await here

            if (foundAccount == null)
            {
                return null;
            }

            var account = new Account
            {
                GameName = gameName,
                GameTag = tagLine,
                Puuid = foundAccount.Puuid // Assuming you want to store the Puuid
            };

            return account;
        }

        public async Task<Summoner> GetSummoner(string puuid, long accountId)
        {
            string url = $"https://eun1.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuid}";
            var foundSummoner = await GetRiotApiData<SummonerDto>(url); // Added await here

            if (foundSummoner == null)
            {
                return null;
            }

            var summoner = new Summoner
            {
                AccountId = accountId,
                EncryptedAccountId = foundSummoner.AccountId,
                ProfileIconId = foundSummoner.ProfileIconId,
                RevisionDate = foundSummoner.RevisionDate,
                SummonerId = foundSummoner.SummonerId,
                Puuid = foundSummoner.Puuid,
                SummonerLevel = foundSummoner.SummonerLevel
            };

            return summoner;
        }
        public async Task<List<string>> GetMatchIds(string puuid, long? startTime, long? endTime, int? queue, string? type, int start = 0, int count = 20)
        {
            string url = $"https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids?start={start}&count={count}";


            // Append optional parameters
            if (startTime.HasValue)
            {
                AppendQueryParam(ref url, "startTime", startTime.Value.ToString());
            }
            if (endTime.HasValue)
            {
                AppendQueryParam(ref url, "endTime", endTime.Value.ToString());
            }
            if (queue.HasValue)
            {
                AppendQueryParam(ref url, "queue", queue.Value.ToString());
            }
            if (!string.IsNullOrEmpty(type))
            {
                AppendQueryParam(ref url, "type", type);
            }

            return await GetRiotApiData<List<string>>(url);
        }
    }
}
