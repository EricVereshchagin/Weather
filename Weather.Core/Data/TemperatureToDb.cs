using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Weather.Core.Data
{
    public class TemperatureToDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("day")]
        public DateTime Day { get; set; }

        [BsonElement("temperature_from")]
        public double TemperatureFrom { get; set; }

        [BsonElement("temperature_to")]
        public double TemperatureTo { get; set; }
    }
}
