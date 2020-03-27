using System.Collections.Generic;
using LB_151.Data;
using LB_151.Models;
using Microsoft.AspNetCore.Mvc;

namespace LB_151.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : Controller
    {
        private readonly AuthenticationService _authenticationService;
        private readonly CategoryDatabaseService _categoryDatabaseService;
        private readonly LeaderboardDatabaseService _leaderboardDatabaseService;
        private readonly GameService _gameService;

        public LeaderboardController(AuthenticationService authenticationService,
            CategoryDatabaseService categoryDatabaseService, GameService gameService,
            LeaderboardDatabaseService leaderboardDatabaseService)
        {
            _authenticationService = authenticationService;
            _categoryDatabaseService = categoryDatabaseService;
            _gameService = gameService;
            _leaderboardDatabaseService = leaderboardDatabaseService;
        }

        [HttpGet]
        public ActionResult<List<LeaderboardEntry>> Get()
        {
            return _leaderboardDatabaseService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetLeaderboardEntry")]
        public ActionResult<LeaderboardEntry> Get(string id)
        {
            var leaderboardEntry = _leaderboardDatabaseService.Get(id);

            if (leaderboardEntry == null)
            {
                return NotFound();
            }

            return Ok(leaderboardEntry);
        }

        [HttpPost]
        public ActionResult<LeaderboardEntry> Create(GameSession session)
        {
            var game = _gameService.GetGame(session.Id);

            var leaderboardEntry = new LeaderboardEntry
            {
                Category = game.GetCategory().Stripped(),
                Score = game.GetPoints(),
                Timestamp = game.GetTimestamp(),
                Duration = game.GetDuration(),
                User = session.Request
            };

            _leaderboardDatabaseService.Create(leaderboardEntry);

            _gameService.DeleteGame(session.Id);

            return CreatedAtRoute("Leaderboard", new {id = leaderboardEntry.Id.ToString()},
                leaderboardEntry);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var authenticationString = HttpContext.Request.Headers["Authorization"];
            if (_authenticationService.IsValid(authenticationString))
            {
                var entry = _leaderboardDatabaseService.Get(id);

                if (entry == null)
                {
                    return NotFound();
                }

                _leaderboardDatabaseService.Remove(entry.Id);

                return NoContent();
            }

            return Unauthorized();
        }
    }
}