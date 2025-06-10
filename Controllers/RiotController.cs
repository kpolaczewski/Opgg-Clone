using Microsoft.AspNetCore.Mvc;
using RiotStatsAPI.Data;
using RiotStatsAPI.Models.Entities;
using RiotStatsAPI.Services;
using Microsoft.EntityFrameworkCore;


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

                return Ok(new
                {
                    Puuid = existingAccount.Puuid,
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

        [HttpGet("refresh-matches/{puuid}")]
        public async Task<IActionResult> RefreshRecentMatches(string puuid)
        {
            try
            {
                var account = await _dbContext.Accounts
                    .FirstOrDefaultAsync(a => a.Puuid == puuid);
                if (account == null)
                {
                    return NotFound(new { error = "Account not found", detail = "The account could not be found in the database." });
                }

                long? startTime = null;
                if (account.lastSynced.HasValue)
                {
                    startTime = new DateTimeOffset(account.lastSynced.Value).ToUnixTimeSeconds();
                }



                var latestMatchesIds = await _riotApiService.GetMatchIds(puuid, startTime, null, null, null, 0, 20);


                List<Matches> newMatches = new List<Matches>();

                foreach (var matchId in latestMatchesIds)
                {

                    if (await _dbContext.Matches.AnyAsync(m => m.GameId == matchId)) continue;

                    var matchDTO = await _riotApiService.GetMatch(matchId);

                    var match = new Matches
                    {
                        AccountId = account.Id,
                        GameId = matchDTO.MetaDataDto.MatchId,
                        GameDuration = matchDTO.InfoDto.GameDuration,
                        GameMode = matchDTO.InfoDto.GameMode,
                        GameCreation = matchDTO.InfoDto.GameCreation,

                        Kills = matchDTO.InfoDto.Participants
                            .Where(p => p.Puuid == puuid)
                            .Select(p => p.Kills).FirstOrDefault(),

                        Assists = matchDTO.InfoDto.Participants
                            .Where(p => p.Puuid == puuid)
                            .Select(p => p.Assists).FirstOrDefault(),

                        Deaths = matchDTO.InfoDto.Participants
                            .Where(p => p.Puuid == puuid)
                            .Select(p => p.Deaths).FirstOrDefault(),

                        Win = matchDTO.InfoDto.Participants
                            .Where(p => p.Puuid == puuid)
                            .Select(p => p.Win).FirstOrDefault(),

                        ChampionName = matchDTO.InfoDto.Participants
                            .Where(p => p.Puuid == puuid)
                            .Select(p => p.ChampionName).FirstOrDefault(),

                        ChampionLevel = matchDTO.InfoDto.Participants
                            .Where(p => p.Puuid == puuid)
                            .Select(p => p.ChampionLevel).FirstOrDefault(),

                        Item0 = matchDTO.InfoDto.Participants
                            .Where(p => p.Puuid == puuid)
                            .Select(p => p.Item0).FirstOrDefault(),

                        Item1 = matchDTO.InfoDto.Participants
                            .Where(p => p.Puuid == puuid)
                            .Select(p => p.Item1).FirstOrDefault(),

                        Item2 = matchDTO.InfoDto.Participants
                            .Where(p => p.Puuid == puuid)
                            .Select(p => p.Item2).FirstOrDefault(),

                        Item3 = matchDTO.InfoDto.Participants
                            .Where(p => p.Puuid == puuid)
                            .Select(p => p.Item3).FirstOrDefault(),

                        Item4 = matchDTO.InfoDto.Participants
                        .Where(p => p.Puuid == puuid)
                        .Select(p => p.Item4).FirstOrDefault(),

                        Item5 = matchDTO.InfoDto.Participants
                            .Where(p => p.Puuid == puuid)
                            .Select(p => p.Item5).FirstOrDefault(),

                        Item6 = matchDTO.InfoDto.Participants
                        .Where(p => p.Puuid == puuid)
                        .Select(p => p.Item6).FirstOrDefault()

                    };
                    foreach(var participantDto in matchDTO.InfoDto.Participants)
                    {
                        var participant = new Participant
                        {
                            ChampionName = participantDto.ChampionName,
                            Assists = participantDto.Assists,
                            Deaths = participantDto.Deaths,
                            Kills = participantDto.Kills,
                            Puuid = participantDto.Puuid,
                            Win = participantDto.Win,
                            RiotIdGameName = participantDto.RiotIdGameName,
                            ChampionLevel = participantDto.ChampionLevel,
                            Item0 = participantDto.Item0,
                            Item1 = participantDto.Item1,
                            Item2 = participantDto.Item2,
                            Item3 = participantDto.Item3,
                            Item4 = participantDto.Item4,
                            Item5 = participantDto.Item5,
                            Item6 = participantDto.Item6,
                            TeamId = participantDto.TeamId,
                            GameId = matchDTO.MetaDataDto.MatchId
                        };

                        _dbContext.Participants.Add(participant);
                        
                    }
                    
                    _dbContext.Matches.Add(match);
                    newMatches.Add(match);

                    if (newMatches.Count > 0)
                    {
                        account.lastSynced = DateTime.UtcNow;
                        await _dbContext.SaveChangesAsync();
                    }
                }

                await _dbContext.SaveChangesAsync();

                var displayedMatches = await _dbContext.Matches
                    .Where(m => m.AccountId == account.Id)
                    .OrderByDescending(m => m.GameCreation)
                    .Take(20)
                    .ToListAsync();

                return Ok(new
                {
                    Matches = displayedMatches,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in RefreshRecentMatches: {ex.Message}");
                return StatusCode(500, new { error = "Failed to refresh matches", detail = ex.Message });
            }
        }

        [HttpGet("match-participants/{gameId}")]
        public async Task<IActionResult> GetMatchParticipants(string gameId)
        {
            var participants = await _dbContext.Participants
                .Where(p => p.GameId == gameId)
                .Select(p => new
                {
                    p.ChampionName,
                    p.Kills,
                    p.Deaths,
                    p.Assists,
                    KDA = p.Deaths == 0 ? p.Kills + p.Assists : (double)(p.Kills + p.Assists) / p.Deaths,
                    p.TeamId,
                    p.RiotIdGameName
                })
                .ToListAsync();

            return Ok(participants);
        }

    }
}