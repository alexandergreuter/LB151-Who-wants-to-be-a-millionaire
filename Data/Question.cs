using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LB_151.Data
{
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement] public string Name { get; set; }
        [BsonElement] public string[] Answers { get; set; }
        [BsonElement] public string Correct { get; set; }

        [BsonElement] public int Right { get; set; }

        [BsonElement] public int Wrong { get; set; }

        public DateTime? Asked { get; set; }

        //Removes the correct answer to avoid cheating
        public Question Stripped()
        {
            Question toReturn = this.Clone();

            toReturn.Correct = null;
            toReturn.Answers = toReturn.Answers.ToList().Shuffle().ToArray();

            return toReturn;
        }

        public Question Clone()
        {
            Question toReturn = new Question();

            toReturn.Id = this.Id;
            toReturn.Name = this.Name;
            toReturn.Answers = this.Answers;
            toReturn.Correct = this.Correct;
            toReturn.Right = this.Right;
            toReturn.Wrong = this.Wrong;
            toReturn.Asked = this.Asked;

            return toReturn;
        }
    }
}