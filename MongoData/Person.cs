using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoData
{
    public class Person
    {
        public Person()
        {
        }

        [BsonId]
        public int Id { get; set; }
        [BsonRequired]
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonRequired]
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonRequired]
        [BsonElement("Age")]
        public int Age { get; set; }
        [BsonRequired]
        [BsonElement("Weight")]
        public int Weight { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<Email> EmailAddresses { get; set; } = new List<Email>();

        public Type ValueType => throw new NotImplementedException();
    }
}
