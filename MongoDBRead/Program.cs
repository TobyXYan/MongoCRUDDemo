using MongoData;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;

namespace MongoDBRead
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var mongoUrl     = new MongoUrlBuilder("mongodb://localhost");
                var dbClient     = new MongoClient(mongoUrl.ToMongoUrl());
                var dataBase     = dbClient.GetDatabase("People");
                var collection   = dataBase.GetCollection<BsonDocument>("Persons");

                //GetFirstDocAsync(collection);
                //Console.WriteLine();
                //GetFirstDocByAgeAsync(collection, 50);
                //Console.WriteLine();
                //GetAllDocsAsync(collection);
                //Console.WriteLine();
                //GetGTEFilteredResultsAsync(collection, 50);
                //Console.WriteLine();
                //LinqFilterExampleAsync(dataBase, 50);
                //Console.WriteLine();
                //ToCursorExampleAsync(collection, 50);
                //Console.WriteLine();
                SortDemoAsync(collection, 50);
                Console.ReadLine();

            }
            catch(Exception e)
            {
            }
            
        }

        private static async void GetFirstDocAsync(IMongoCollection<BsonDocument> collection)
        {
            Console.WriteLine("Enter the function of GetFirstDocAsync");
            var doc = await collection.Find(new BsonDocument()).FirstOrDefaultAsync();
            var person = (Person)BsonSerializer.Deserialize(doc, typeof(Person));
            Console.WriteLine($"The first one is {person.FirstName}.{person.LastName}");
        }

        private static async void GetFirstDocByAgeAsync(IMongoCollection<BsonDocument> collection, int age)
        {
            Console.WriteLine("Enter the function of GetFirstDocByAgeAsync");
            var fileter = Builders<BsonDocument>.Filter.Eq("Age", age);
            var doc = await collection.Find(fileter).FirstOrDefaultAsync();
            var person = (Person)BsonSerializer.Deserialize(doc, typeof(Person));
            Console.WriteLine($"The first one of age {age} is {person.FirstName}.{person.LastName}, whose Weight is {person.Weight}");
        }

        private static async void GetAllDocsAsync(IMongoCollection<BsonDocument> collection)
        {
             Console.WriteLine("Enter the function of GetAllDocsAsync");
             var docs = await collection.Find(new BsonDocument()).ToListAsync();
            foreach(var doc in docs)
            {
                var person = (Person)BsonSerializer.Deserialize(doc, typeof(Person));
                Console.WriteLine($"This is {person.FirstName}.{person.LastName}");
            }
        }

        private static async void GetGTEFilteredResultsAsync(IMongoCollection<BsonDocument> collection, int age)
        {
            Console.WriteLine("Enter the function of GetGTEFilteredResultsAsync");
            var oldAgeFilter = Builders<BsonDocument>.Filter.Gte("Age", age);
            var elders = await collection.Find(oldAgeFilter).ToListAsync();
            foreach (var elder in elders)
            {
                var person = (Person)BsonSerializer.Deserialize(elder, typeof(Person));
                Console.WriteLine($"{person.FirstName}'s age is {person.Age}");
            }
        }

        private static async void LinqFilterExampleAsync(IMongoDatabase mongoDatabase, int age)
        {
            Console.WriteLine("Enter the function of LinqFilterExampleAsync");
            var collection = mongoDatabase.GetCollection<Person>("Persons");//Kinda of Serializer deserialized to Entities
            var elders = await collection.AsQueryable<Person>().Where(e => e.Age >= age).ToListAsync();//Then we can use linq to query
            foreach(var elder in elders)
                Console.WriteLine($"{elder.FirstName}'s age is {elder.Age}");
        }

        private static async void ToCursorExampleAsync(IMongoCollection<BsonDocument> collection, int age)
        {
            Console.WriteLine("Enter the function of ToCursorExampleAsync");
            var oldAgeFilter = Builders<BsonDocument>.Filter.Gte("Age", age);
            var eldersCursor = await collection.Find(oldAgeFilter).ToCursorAsync();
            foreach(var elder in eldersCursor.ToEnumerable())
            {
                var person = (Person)BsonSerializer.Deserialize(elder, typeof(Person));
                Console.WriteLine($"{person.FirstName}'s age is {person.Age}");
            }
        }

        private static async void SortDemoAsync(IMongoCollection<BsonDocument> collection, int age)
        {
            Console.WriteLine("Enter the function of SortDemoAsync");
            var oldAgeFilter = Builders<BsonDocument>.Filter.Gte("Age", age);
            var sort = Builders<BsonDocument>.Sort.Descending("FirstName");
            var sortedElders = await collection.Find(oldAgeFilter).Sort(sort).ToListAsync();
            foreach(var elder in sortedElders)
            {
                var person = (Person)BsonSerializer.Deserialize(elder, typeof(Person));
                Console.WriteLine($"{person.FirstName}'s age is {person.Age}");
            }
        }
    }
}
