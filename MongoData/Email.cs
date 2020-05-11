using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoData
{
    public class Email
    {
        //public int Id { get; set; }//Remove Id here cause in Mongo only the root document should have Id
        [BsonRequired]
        [BsonElement("EmailAddress")]
        public string EmailAddress { get; set; }
    }
}
