using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LB_151.Data
{
    public class LeaderboardEntry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement] public string User { get; set; }

        [BsonElement] public int Score { get; set; }

        [BsonElement] public DateTime Timestamp { get; set; }

        [BsonElement] public Category Category { get; set; }
        [BsonElement] public double Duration { get; set; }
    }
}