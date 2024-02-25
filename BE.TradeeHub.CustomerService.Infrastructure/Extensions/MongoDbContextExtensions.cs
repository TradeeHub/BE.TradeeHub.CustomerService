using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Entities.Reference;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Infrastructure.Extensions;

public static class MongoDbContextExtensions
{
    public static void EnsureIndexesCreated(this IMongoDbContext dbContext)
    {
        // Assuming each entity has a UserOwnerId or another relevant field for indexing
        // Customers
        var customersIndexKeys = Builders<CustomerEntity>.IndexKeys.Ascending(c => c.UserOwnerId);
        var customersIndexModel = new CreateIndexModel<CustomerEntity>(customersIndexKeys);
        dbContext.Customers.Indexes.CreateOne(customersIndexModel);

        // CustomerReferenceNumbers
        var customerReferenceNumbersIndexKeys = Builders<CustomerReferenceNumberEntity>.IndexKeys.Ascending(crn => crn.UserOwnerId);
        var customerReferenceNumbersIndexModel = new CreateIndexModel<CustomerReferenceNumberEntity>(customerReferenceNumbersIndexKeys);
        dbContext.CustomerReferenceNumber.Indexes.CreateOne(customerReferenceNumbersIndexModel);

        // Properties
        var propertiesIndexKeys = Builders<PropertyEntity>.IndexKeys.Ascending(p => p.UserOwnerId);
        var propertiesIndexModel = new CreateIndexModel<PropertyEntity>(propertiesIndexKeys);
        dbContext.Properties.Indexes.CreateOne(propertiesIndexModel);

        // Comments
        var commentsIndexKeys = Builders<CommentEntity>.IndexKeys.Ascending(c => c.UserOwnerId);
        var commentsIndexModel = new CreateIndexModel<CommentEntity>(commentsIndexKeys);
        dbContext.Comments.Indexes.CreateOne(commentsIndexModel);

        // ExternalReferences
        var externalReferencesIndexKeys = Builders<ExternalReferenceEntity>.IndexKeys.Ascending(er => er.UserOwnerId);
        var externalReferencesIndexModel = new CreateIndexModel<ExternalReferenceEntity>(externalReferencesIndexKeys);
        dbContext.ExternalReferences.Indexes.CreateOne(externalReferencesIndexModel);
    }
    
    public static void AddMongoDbCollections(this IServiceCollection services)
    {
        services.AddSingleton<IMongoCollection<CustomerEntity>>(sp => sp.GetRequiredService<IMongoDbContext>().Customers);
        services.AddSingleton<IMongoCollection<CustomerReferenceNumberEntity>>(sp => sp.GetRequiredService<IMongoDbContext>().CustomerReferenceNumber);
        services.AddSingleton<IMongoCollection<PropertyEntity>>(sp => sp.GetRequiredService<IMongoDbContext>().Properties);
        services.AddSingleton<IMongoCollection<CommentEntity>>(sp => sp.GetRequiredService<IMongoDbContext>().Comments);
        services.AddSingleton<IMongoCollection<ExternalReferenceEntity>>(sp => sp.GetRequiredService<IMongoDbContext>().ExternalReferences);
    }
}