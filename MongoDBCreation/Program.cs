using MongoData;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBCreation
{
    class Program
    {
        public const string StrGeneratedJson = "generated.json";

        static void Main(string[] args)
        {
            IMongoDatabase mongoDatabase = null;
            var mongoUrl = new MongoUrlBuilder("mongodb://localhost");
            var dbClient = new MongoClient(mongoUrl.ToMongoUrl());
            var dbNames = dbClient.ListDatabaseNames().ToList();
            do
            {
                if (dbNames.Contains("People"))
                    break;
                mongoDatabase = dbClient.GetDatabase("People");
                mongoDatabase.CreateCollectionAsync("Persons");
                mongoDatabase.CreateCollection("Counter");
                
            } while (false);

            var file   = File.ReadAllText(StrGeneratedJson);
            var people = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Person>>(file);

            mongoDatabase = GetPeopleDb();
            if (null == mongoDatabase)
                return;

            var counterCollection = mongoDatabase.GetCollection<BsonDocument>("Counter");
            if(counterCollection.CountDocuments(new BsonDocument()) == 0)
                CreateCounter(counterCollection);

            var collection = mongoDatabase.GetCollection<BsonDocument>("Persons");
            if (collection.CountDocuments(new BsonDocument()) > 0)
                return;
            foreach(var person in people)
            {
                person.Id = GetAndUpdateId();
                var bDoc = person.ToBsonDocument();
                collection.InsertOne(bDoc);
            }
            Console.ReadLine();
        }

        private static void CreateCounter(IMongoCollection<BsonDocument> counterCollection)
        {
            var counter = new Counter() { NextId = 1 };
            counterCollection.InsertOne(counter.ToBsonDocument());
        }

        private static int GetAndUpdateId( )
        {
            var mongoDb = GetPeopleDb();
            var counterCollection = mongoDb.GetCollection<BsonDocument>("Counter");
            var counterDoc = counterCollection.Find(new BsonDocument()).FirstOrDefault();
            var counter = BsonSerializer.Deserialize<Counter>(counterDoc);
            counterCollection.UpdateOne(Builders<BsonDocument>.Filter.AnyGte("NextId",0),Builders<BsonDocument>.Update.Set("NextId",counter.NextId+1));
            return counter.NextId;
        }

        private static IMongoDatabase GetPeopleDb()
        {
            var mongoUrl = new MongoUrlBuilder("mongodb://localhost");
            var dbClient = new MongoClient(mongoUrl.ToMongoUrl());
            return dbClient.GetDatabase("People");
        }
    }
}

