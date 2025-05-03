using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiotStatsAPI.Data;
using RiotStatsAPI.Models.Entities;
using RiotStatsAPI.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RiotStatsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiotController : ControllerBase
    {
        private readonly RiotApiService _riotApiService;
        private readonly AppDbContext _dbContext;
        private readonly ILogger<RiotController> _logger;

        public RiotController(RiotApiService riotApiService, AppDbContext dbContext, ILogger<RiotController> logger)
        {
            _riotApiService = riotApiService;
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("account/{gameName}/{tagLine}")]
        public async Task<IActionResult> GetAccountAndSummoner(string gameName, string tagLine)
        {
            try
            {
                // Step 1: Check if account exists in the DB
                var existingAccount = await _dbContext.Accounts
                    .FirstOrDefaultAsync(a => a.GameName == gameName && a.GameTag == tagLine);

                if (existingAccount == null)
                {
                    // Account not found in DB, fetch from Riot API
                    var account = await _riotApiService.GetAccount(gameName, tagLine);

                    if (account == null)
                    {
                        return NotFound(new { error = "Account not found", detail = "The account could not be found in the Riot API." });
                    }

                    // Save the account to DB
                    _dbContext.Accounts.Add(account);
                    await _dbContext.SaveChangesAsync();

                    existingAccount = account;  // Set the existingAccount to the newly created one
                }

                // Step 2: Check if summoner exists in the DB
                var existingSummoner = await _dbContext.Summoners
                    .FirstOrDefaultAsync(s => s.Puuid == existingAccount.Puuid && s.AccountId == existingAccount.Id);

                if (existingSummoner == null)
                {
                    // Summoner not found in DB, fetch from Riot API
                    var summoner = await _riotApiService.GetSummoner(existingAccount.Puuid, existingAccount.Id);

                    if (summoner == null)
                    {
                        return NotFound(new { error = "Summoner not found", detail = "The summoner could not be found in the Riot API." });
                    }

                    // Save the summoner to DB
                    _dbContext.Summoners.Add(summoner);
                    await _dbContext.SaveChangesAsync();

                    existingSummoner = summoner;  // Set the existingSummoner to the newly created one
                }

                // Step 3: Return summoner data
                return Ok(new
                {
                    SummonerId = existingSummoner.SummonerId,
                    SummonerLevel = existingSummoner.SummonerLevel,
                    ProfileIconId = existingSummoner.ProfileIconId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAccountAndSummoner: {ex.Message}");
                return StatusCode(500, new { error = "Failed to fetch account and summoner data", detail = ex.Message });
            }
        }
    }
}
