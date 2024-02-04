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
        // CreateIndexes();
    }
    
    public IMongoCollection<CustomerDbObject> Customers => _database.GetCollection<CustomerDbObject>("Customers");
    public IMongoCollection<PropertyDbObject> Properties => _database.GetCollection<PropertyDbObject>("Properties");
    public IMongoCollection<CommentDbObject> Comments => _database.GetCollection<CommentDbObject>("Comments");
    
    private void CreateIndexes()
    {
        var customersCollection = _database.GetCollection<CustomerDbObject>("Customers");
        var customerIndexModel = new CreateIndexModel<CustomerDbObject>(
            Builders<CustomerDbObject>.IndexKeys.Ascending(customer => customer.UserOwnerId)); // Specify the field you want to index
        customersCollection.Indexes.CreateOne(customerIndexModel);

        var propertiesCollection = _database.GetCollection<PropertyDbObject>("Properties");
        var propertyIndexModel = new CreateIndexModel<PropertyDbObject>(
            Builders<PropertyDbObject>.IndexKeys.Ascending(property => property.UserOwnerId)); // Specify the field you want to index
        propertiesCollection.Indexes.CreateOne(propertyIndexModel);
        
        var commentsCollection = _database.GetCollection<CommentDbObject>("Comments");
        var commentIndexModel = new CreateIndexModel<CommentDbObject>(
            Builders<CommentDbObject>.IndexKeys
                .Ascending(comment => comment.CustomerId)
                .Ascending(comment => comment.UserOwnerId));

        commentsCollection.Indexes.CreateOne(commentIndexModel);
    }
}