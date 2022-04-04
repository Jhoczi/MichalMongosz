using MongoDriverUsing.Db.Interfaces;
using MongoDriverUsing.Models;

namespace MongoDriverUsing.Services;

public class TankService : ITankService
{
    private readonly IMongoRepository<Tank> _tankRepository;
    
    public TankService(IMongoRepository<Tank> tankRepository)
    {
        _tankRepository = tankRepository;
    }
    
    public async Task<IEnumerable<Tank>> GetAllTanks()
    {
        return _tankRepository.FilterBy(x => x.Id != null);
    }
    
}

public interface ITankService
{
    Task<IEnumerable<Tank>> GetAllTanks();
}