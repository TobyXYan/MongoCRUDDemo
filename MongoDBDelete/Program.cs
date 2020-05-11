using MongoData;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBDelete
{ 
    class Program
    {
        static void Main(string[] args)
        {
            var mongoUrl    = new MongoUrlBuilder("mongodb://localhost");
            var mongoClient = new MongoClient(mongoUrl.ToMongoUrl());
            var mongoDb     = mongoClient.GetDatabase("People");
            var collection  = mongoDb.GetCollection<BsonDocument>("Persons");
            //DeleteOne(collection);
            //DeleteOneWithLinq(mongoDb);
            DeleteAll(collection);
        }

        private static void DeleteOne(IMongoCollection<BsonDocument> collection)
        {
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id",2);
            collection.DeleteOne(deleteFilter);
        }

        private static void DeleteOneWithLinq(IMongoDatabase mongoDb)
        {
            var collection = mongoDb.GetCollection<Person>("Persons");
            collection.DeleteOne(x=>x.Id == 2);
        }

        private static void DeleteAll(IMongoCollection<BsonDocument> collection)
        {
            collection.DeleteMany(new BsonDocument());
        }
    }
}
