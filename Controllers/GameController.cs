using System;
using System.Collections.Generic;
using LB_151.Data;
using LB_151.Models;
using Microsoft.AspNetCore.Mvc;

namespace LB_151.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly AuthenticationService _authenticationService;
        private readonly GameService _gameService;
        private readonly CategoryDatabaseService _categoryDatabaseService;

        public GameController(AuthenticationService authenticationService,
            GameService gameService, CategoryDatabaseService categoryDatabaseService)
        {
            _authenticationService = authenticationService;
            _gameService = gameService;
            _categoryDatabaseService = categoryDatabaseService;
        }
        
        [HttpPost("Start")]
        public ActionResult<GameSession> StartGame([FromBody] Category category)
        {
            var session = new GameSession();

            // Ensures that the Category isn't stripped since it relies on certain values
            session.Id = _gameService.CreateGame(_categoryDatabaseService.Get(category.Id));
            session.Points = _gameService.GetGame(session.Id).StartGame().GetPoints();

            return Ok(session);
        }

        [HttpPost("Question")]
        public ActionResult<Question> GetQuestion([FromBody] GameSession session)
        {
            var game = _gameService.GetGame(session.Id);

            return Ok(game.GetCurrentQuestion().Stripped());
        }

        [HttpPost("Answer")]
        public ActionResult<GameSession> SubmitAnswer([FromBody] GameSession session)
        {
            var game = _gameService.GetGame(session.Id);

            game.SubmitAnswer(session.Request);
            session.Points = game.GetPoints();

            return Ok(session);
        }

        [HttpPost("Result")]
        public ActionResult<Result> GetResult([FromBody] GameSession session)
        {
            var game = _gameService.GetGame(session.Id);

            return Ok(game.GetCurrentResult());
        }

        [HttpPost("Joker")]
        public ActionResult<string[]> UseJoker([FromBody] GameSession session)
        {
            var game = _gameService.GetGame(session.Id);

            return Ok(game.UseJoker());
        }
    }

    public class GameSession
    {
        public string Id { get; set; }
        public string Request { get; set; }
        public int Points { get; set; }
    }
}