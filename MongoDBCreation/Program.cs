using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDBCreation
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongoUrl = new MongoUrlBuilder("mongodb://localhost");
            var dbClient = new MongoClient(mongoUrl.ToMongoUrl());
            var dbNames = dbClient.ListDatabaseNames().ToList();
            if(dbNames.Contains("People"))
                return;
            var database = dbClient.GetDatabase("People");
            database.CreateCollectionAsync("Persons");
            Console.ReadLine();
        }
    }
}

