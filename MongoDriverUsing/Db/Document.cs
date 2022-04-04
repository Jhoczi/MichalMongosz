using MongoDB.Bson;
using MongoDriverUsing.Db.Interfaces;

namespace MongoDriverUsing.Db;

public class Document : IDocument
{
    public ObjectId Id { get; set; }
    public DateTime CreatedAt => Id.CreationTime;
}
