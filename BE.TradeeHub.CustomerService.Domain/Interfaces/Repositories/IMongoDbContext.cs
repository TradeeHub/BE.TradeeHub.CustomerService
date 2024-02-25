using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Entities.Reference;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;

public interface IMongoDbContext
{
    IMongoClient Client { get; }
    IMongoCollection<CustomerEntity> Customers { get; }
    IMongoCollection<CustomerReferenceNumberEntity> CustomerReferenceNumber { get; }
    IMongoCollection<PropertyEntity> Properties { get; }
    IMongoCollection<CommentEntity> Comments { get; }
    IMongoCollection<ExternalReferenceEntity> ExternalReferences { get; }
}