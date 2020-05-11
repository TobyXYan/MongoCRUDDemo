using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoData
{
    public class Address
    {
        //public int Id { get; set; }//Remove Id here cause in Mongo only the root document should have Id
        [BsonRequired]
        [BsonElement("StreetAddress")]
        public string StreetAddress { get; set; }
        [BsonRequired]
        [BsonElement("City")]
        public string City { get; set; }
        [BsonRequired]
        [BsonElement("State")]
        public string State { get; set; }
        [BsonRequired]
        [BsonElement("ZipCode")]
        public string ZipCode { get; set; }
    }
}
