using BE.TradeeHub.CustomerService.Domain.Interfaces;
using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Infrastructure;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    
    public MongoDbContext(IAppSettings appSettings)
    {
        var settings = MongoClientSettings.FromConnectionString(appSettings.MongoDbConnectionString);
        settings.GuidRepresentation = GuidRepresentation.Standard;

        // Now create the MongoClient using the configured settings.
        var client = new MongoClient(settings);
        
        _database = client.GetDatabase(appSettings.MongoDbDatabaseName);
    }
    
    public IMongoCollection<CustomerDbObject> Customers => _database.GetCollection<CustomerDbObject>("Customers");
    public IMongoCollection<PropertyDbObject> Properties => _database.GetCollection<PropertyDbObject>("Properties");
    
}