using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Infrastructure;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("MongoDB:ConnectionString").Value;
        var databaseName = configuration.GetSection("MongoDB:DatabaseName").Value;
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }
    
    public IMongoCollection<CustomerDbObject> Customers => _database.GetCollection<CustomerDbObject>("Customers");
    public IMongoCollection<PropertyDbObject> Properties => _database.GetCollection<PropertyDbObject>("Properties");

    // public IMongoCollection<T> GetCollection<T>(string name)
    // {
    //     return _database.GetCollection<T>(name);
    // }
}