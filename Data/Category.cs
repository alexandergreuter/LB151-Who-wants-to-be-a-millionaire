using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LB_151.Data
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement] public string Name { get; set; }

        [BsonElement] public Question[] Questions { get; set; }

        //Removes the questions to avoid cheating
        public Category Stripped()
        {
            Category toReturn = this.Clone();

            toReturn.Questions = null;

            return toReturn;
        }
        
        public Category Clone()
        {
            Category toReturn = new Category();

            toReturn.Id = this.Id;
            toReturn.Name = this.Name;
            toReturn.Questions = this.Questions;

            return toReturn;
        }
    }
}