using BE.TradeeHub.CustomerService.Domain.SubgraphEntities;
using HotChocolate.Types.Relay;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class PropertyEntity
{
    [ID]
    [BsonId] public ObjectId Id { get; set; }
    public Guid UserOwnerId { get; set; }
    public PlaceEntity Property { get; set; } = null!;
    public PlaceEntity? Billing { get; set; }
    public List<ObjectId> Customers { get; set; } = null!;
    public List<ObjectId>? Quotes { get; set; }
    public List<ObjectId>? Jobs { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public UserEntity Owner() => new UserEntity { Id = UserOwnerId };
    public UserEntity Creator () => new UserEntity { Id = CreatedBy };
    public UserEntity? Modifier () => ModifiedBy.HasValue ? new UserEntity { Id = ModifiedBy.Value } : null;
    
    public PropertyEntity()
    {
    }
    
    public PropertyEntity(Guid userOwnerId, PlaceEntity property, PlaceEntity? billing, Guid createdBy, bool isBillingAddress)
    {
        UserOwnerId = userOwnerId;
        Property = property;
        Billing = isBillingAddress && billing == null ? property : billing;
        Customers = [];
        Quotes = [];
        Jobs = [];
        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
    }
}