using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDriverUsing.Services;

namespace MongoDriverUsing;

public class Worker
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    // create constructor
    public Worker(ILogger<Worker> logger, IConfiguration configuration, IUserService userService)
    {
        _logger = logger;
        _configuration = configuration;
        _userService = userService;
    }
    
    public async Task Run()
    {
        _logger.LogInformation("Hello World!");
        var result = await _userService.GetAll();
        Console.WriteLine(result.ElementAt(0).Name);
    }
}