using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDriverUsing.Models;
using MongoDriverUsing.Services;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureAppConfiguration((context, config) =>
{
    var dupa = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    config.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty);
    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
});

builder.ConfigureServices((context, services) =>
{
    services.Configure<MongoDbSettings>(context.Configuration.GetSection("MongoDb"));
    services.AddSingleton<IUserService,MongoDbService>();
});


var app = builder.Build();

IUserService? userService = app.Services.GetService<IUserService>();
var result = await userService.GetAll();
Console.WriteLine(result.ElementAt(0).Name);

await app.RunAsync();




