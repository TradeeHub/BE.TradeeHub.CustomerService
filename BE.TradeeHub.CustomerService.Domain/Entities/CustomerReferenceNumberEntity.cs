using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class CustomerReferenceNumberEntity
{
    [BsonId]
    public ObjectId Id { get; set; }
    public Guid UserOwnerId { get; set; }
    public int Counter { get; set; }
    
}