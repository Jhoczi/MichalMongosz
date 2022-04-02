using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDriverUsing;
using MongoDriverUsing.Models;
using MongoDriverUsing.Services;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureAppConfiguration((context, config) =>
{
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    config.AddEnvironmentVariables();
});

builder.ConfigureServices((context, services) =>
{
    services.Configure<MongoDbSettings>(context.Configuration.GetSection("MongoDb"));
    services.AddSingleton<IUserService,MongoDbService>();
});


var app = builder.Build();

var worker = ActivatorUtilities.CreateInstance<Worker>(app.Services);
worker.Run();
await app.RunAsync();




