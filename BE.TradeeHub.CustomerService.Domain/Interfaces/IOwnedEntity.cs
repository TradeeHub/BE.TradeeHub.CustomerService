using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces;

public interface IOwnedEntity
{
    ObjectId Id { get; set; }
    Guid UserOwnerId { get; set; }
}