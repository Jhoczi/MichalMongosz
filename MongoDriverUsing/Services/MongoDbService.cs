using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDriverUsing.Models;

namespace MongoDriverUsing.Services;

public interface IMongoDbService<T> where T: class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(string id);
    Task Add(T item);
    Task<bool> Remove(string id);
    Task<bool> Update(string id, T item);
}

// generate interface for crud operations on user
public interface IUserService
{
    Task<IEnumerable<User>> GetAll();
    Task<User> Get(string id);
    Task Add(User item);
    Task<bool> Remove(string id);
    Task<bool> Update(string id, User item);
}

public class MongoDbService : IUserService
{
    private readonly IMongoCollection<User> _collection;

    public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _collection = database.GetCollection<User>(mongoDbSettings.Value.CollectionName);
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
}