using System.Collections.Generic;
using LB_151.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LB_151.Models
{
    public class CategoryDatabaseService
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryDatabaseService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _categories = database.GetCollection<Category>(settings.CategoryCollectionName);
        }

        public List<Category> Get()
        {
            return _categories.Find(category => true).ToList();
        }

        public List<Category> GetStripped()
        {
            var toReturn = new List<Category>();

            foreach (var category in _categories.Find(category => true).ToList())
            {
                toReturn.Add(category.Stripped());
            }

            return toReturn;
        }

        public Category Get(string id)
        {
            return _categories.Find<Category>(category => category.Id == id).FirstOrDefault();
        }

        public Category GetStripped(string id)
        {
            return _categories.Find<Category>(category => category.Id == id).FirstOrDefault().Stripped();
        }

        public Category Create(Category category)
        {
            _categories.InsertOne(category);
            return category;
        }

        public void Update(string id, Category categoryIn)
        {
            _categories.ReplaceOne(category => category.Id == id, categoryIn);
        }

        public void Remove(Category categoryIn)
        {
            _categories.DeleteOne(category => category.Id == categoryIn.Id);
        }

        public void Remove(string id)
        {
            _categories.DeleteOne(category => category.Id == id);
        }
    }
}