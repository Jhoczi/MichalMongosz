using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDriverUsing.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [BsonElement("age")]
    [JsonPropertyName("age")]
    public int Age { get; set; }
}