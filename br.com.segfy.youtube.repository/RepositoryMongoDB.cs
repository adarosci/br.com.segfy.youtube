using br.com.segfy.youtube.domain.interfaces;
using br.com.segfy.youtube.infra;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace br.com.segfy.youtube.repository
{
    public class RepositoryMongoDB : IRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;

        public RepositoryMongoDB(IOptions<AppSettings> options)
        {
            _client = new MongoClient(options.Value.ConnectionString);
            _db = _client.GetDatabase("YoutubeDB");
        }

        public void Save<T>(T obj, string collection)
        {
            var mongoCollection = _db.GetCollection<T>(collection);
            mongoCollection.InsertOne(obj);
        }

        public void Save<T>(IEnumerable<T> obj, string collection)
        {
            var mongoCollection = _db.GetCollection<T>(collection);
            mongoCollection.InsertMany(obj);
        }

        public IEnumerable<T> Load<T>(string collection)
        {
            var mongoCollection = _db.GetCollection<T>(collection);
            return mongoCollection.AsQueryable().ToList();
        }
        public IEnumerable<T> Load<T>(Func<T, bool> predicate, string collection = null)
        {
            var mongoCollection = _db.GetCollection<T>(collection);
            return mongoCollection.AsQueryable().Where(predicate).ToList();
        }
    }
}
