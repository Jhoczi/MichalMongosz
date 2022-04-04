using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDriverUsing.Services;

namespace MongoDriverUsing;

public class Worker
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly ITankService _tankService;

    // create constructor
    public Worker(ILogger<Worker> logger, IConfiguration configuration, IUserService userService, ITankService tankService)
    {
        _logger = logger;
        _configuration = configuration;
        _userService = userService;
        _tankService = tankService;
    }
    
    public async Task Run()
    {
        _logger.LogInformation("Hello World!");
        // var result = await _userService.GetAll();
        // Console.WriteLine($"User first name: {result.ElementAt(0).Name}");
        //await _userService.MongoFilteredAsBsonDocument();
        var result = (await _tankService.GetAllTanks()).ToList();
        foreach (var tank in result)
        {
            // write all tank properties
            Console.WriteLine($"Tank name: {tank.Name}");
            Console.WriteLine($"Tank type: {tank.Type}");
            Console.WriteLine($"Tank country: {tank.Country}");
            Console.WriteLine($"Tank year: {tank.YearOfCreation}");
            Console.WriteLine($"Tank crew: {tank.Crew}");
            
        }
    }
}