using MongoData;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace MongoDBUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongoUrl   = new MongoUrlBuilder("mongodb://localhost");
            var dbClient   = new MongoClient(mongoUrl.ToMongoUrl());
            var mongoDb    = dbClient.GetDatabase("People");
            var collection = mongoDb.GetCollection<BsonDocument>("Persons");
            //UpdateWeight(collection);
            //UpdateWeightWithLinq(mongoDb);
            UpdateArrayItem(collection);
        }

        private static void UpdateWeight(IMongoCollection<BsonDocument> collection)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id",1);
            var update = Builders<BsonDocument>.Update.Set("Weight", 60);
            collection.UpdateOne(filter, update);
        }

        private static void UpdateWeightWithLinq(IMongoDatabase mongoDb)
        {
             Console.WriteLine("Enter the function of LinqFilterExampleAsync");
            var collection = mongoDb.GetCollection<Person>("Persons");//Kinda of Serializer deserialized to Entities          
            collection.UpdateOne(x=>x.Id ==1, Builders<Person>.Update.Set("Weight", 70) );
        }

        private static void UpdateArrayItem(IMongoCollection<BsonDocument> collection)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", 1) & Builders<BsonDocument>.Filter.Eq("EmailAddresses.EmailAddress", "dorseythornton@bedder.com");
            collection.UpdateOne(filter, Builders<BsonDocument>.Update.Set("EmailAddresses.$.EmailAddress", "Toby.Yan@163.com") );

        }
    }
}
