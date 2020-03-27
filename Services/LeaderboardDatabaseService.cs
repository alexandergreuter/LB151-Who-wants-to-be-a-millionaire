using System.Collections.Generic;
using LB_151.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LB_151.Models
{
    public class LeaderboardDatabaseService
    {
        private readonly IMongoCollection<LeaderboardEntry> _categories;

        public LeaderboardDatabaseService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _categories = database.GetCollection<LeaderboardEntry>(settings.LeaderboardCollectionName);
        }

        public List<LeaderboardEntry> Get()
        {
            return _categories.Find(leaderboardEntry => true).ToList();
        }

        public LeaderboardEntry Get(string id)
        {
            return _categories.Find<LeaderboardEntry>(leaderboardEntry => leaderboardEntry.Id == id).FirstOrDefault();
        }

        public LeaderboardEntry Create(LeaderboardEntry leaderboardEntry)
        {
            _categories.InsertOne(leaderboardEntry);
            return leaderboardEntry;
        }

        public void Update(string id, LeaderboardEntry leaderboardEntryIn)
        {
            _categories.ReplaceOne(leaderboardEntry => leaderboardEntry.Id == id, leaderboardEntryIn);
        }

        public void Remove(LeaderboardEntry leaderboardEntryIn)
        {
            _categories.DeleteOne(leaderboardEntry => leaderboardEntry.Id == leaderboardEntryIn.Id);
        }

        public void Remove(string id)
        {
            _categories.DeleteOne(leaderboardEntry => leaderboardEntry.Id == id);
        }
    }
}