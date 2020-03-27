using System;
using System.Collections.Generic;
using LB_151.Data;

namespace LB_151.Models
{
    public class GameService
    {
        private readonly CategoryDatabaseService _categoryDatabaseService;
        private Dictionary<string, Game> _games = new Dictionary<string, Game>();

        public GameService(CategoryDatabaseService categoryDatabaseService)
        {
            _categoryDatabaseService = categoryDatabaseService;
        }

        public string CreateGame(Category category)
        {
            Random random = new Random();

            var game = new Game(category, _categoryDatabaseService);
            var toReturn = random.RandomString(30);

            _games.Add(toReturn, game);

            return toReturn;
        }

        public Game GetGame(string id)
        {
            return _games[id];
        }

        public Game DeleteGame(string id)
        {
            var toReturn = _games[id];

            _games.Remove(id);

            return toReturn;
        }
    }
}