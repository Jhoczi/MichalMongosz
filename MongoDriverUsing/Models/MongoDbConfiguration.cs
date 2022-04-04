namespace MongoDriverUsing.Models;

public interface IMongoDbConfiguration
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}
public class MongoDbConfiguration : IMongoDbConfiguration
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}

