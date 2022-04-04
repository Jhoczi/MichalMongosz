using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDriverUsing.Models;

namespace MongoDriverUsing.Services;


// generate interface for crud operations on user
public interface IUserService
{
    Task<IEnumerable<User>> GetAll();
    Task<User> Get(string id);
    Task Add(User item);
    Task<bool> Remove(string id);
    Task<bool> Update(string id, User item);
    Task MongoFilteredAsBsonDocument();
}

public class MongoDbService : IUserService
{
    private readonly IMongoCollection<User> _collection;
    private readonly IMongoDatabase _database;

    public MongoDbService(IMongoDbConfiguration mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.ConnectionString);
        _database = client.GetDatabase(mongoDbSettings.DatabaseName);
        _collection = _database.GetCollection<User>("users");
    }

    
    
    // public async Task<List<User>> GetAllAsync() =>
    //     await _collection.Find(user => true).ToListAsync();
    // public async Task<User> GetAsync(string id) =>
    //     await _collection.Find(user => user.Id == id).FirstOrDefaultAsync();
    // public async Task CreateAsync(User users) =>
    //     await _collection.InsertOneAsync(users);
    // public async Task DeleteAsync(string id) =>
    //     await _collection.DeleteOneAsync(user => user.Id == id);

    // public async Task<IEnumerable<T>> GetAll() =>
    //     await _collection.Find(user => true).ToListAsync();
    //
    // public async Task<T> Get(string id) =>
    //     await _collection.Find({ x => x.}).FirstOrDefaultAsync();
    //
    // public async Task Add(T item) =>
    //     await _collection.InsertOneAsync(item);
    //
    // public async Task<bool> Remove(string id) =>
    //     await _collection.DeleteOneAsync(user => user.Id == id).IsCompletedSuccessfully;
    //
    // public async Task<bool> Update(string id, T item) =>
    //     await _collection.ReplaceOneAsync(user => user.Id == id, item).IsCompletedSuccessfully;
    //
    public async Task<IEnumerable<User>> GetAll() =>
        await _collection.Find(user => true).ToListAsync();

    public async Task<User> Get(string id) =>
        await _collection.Find(user => user.Id == id).FirstOrDefaultAsync();

    public async Task Add(User item) =>
        await _collection.InsertOneAsync(item);

    public async Task<bool> Remove(string id)
    {
        var result = await _collection.DeleteOneAsync(user => user.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
    
    public async Task<bool> Update(string id, User item)
    {
        var result = await _collection.ReplaceOneAsync(user => user.Id == id, item);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task MongoFilteredAsBsonDocument()
    {
        var colectionAsBsonDocument = _database.GetCollection<BsonDocument>("new_users");
        var filter = new BsonDocument("name", "Bill");
        var filteredPeople = await colectionAsBsonDocument.Find(filter).ToListAsync();
        foreach (var person in filteredPeople)
        {
            Console.WriteLine(person);
        }

        var newFilter =
            new BsonDocument("$or",
                new BsonArray()
                {
                    new BsonDocument("Age", new BsonDocument("$gt", 32)),
                    new BsonDocument("Name", "Bill")
                });

        var filteredPeople2 = await colectionAsBsonDocument.Find(newFilter).ToListAsync();
       Console.WriteLine("===============================");
       foreach (var person in filteredPeople2)
       {
           Console.WriteLine($"New result: {person}");
       }
    }
}