using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDriverUsing.Db;

namespace MongoDriverUsing.Models;
[BsonCollection("tanks")]
public class Tank : Document
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [BsonElement("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [BsonElement("country")]
    [JsonPropertyName("country")]
    public string Country { get; set; }
    
    [BsonElement("year")]
    [JsonPropertyName("year")]
    public int YearOfCreation { get; set; }
    
    [BsonElement("crew")]
    [JsonPropertyName("crew")]
    public int Crew { get; set; }
}