using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MongoData
{
    public class Counter
    {
        public Counter()
        { 
            Id = ObjectId.GenerateNewId();
        }
        [BsonId (IdGenerator =typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }
        [BsonElement("NextId")]
        public int NextId { get; set; }
    }
}
