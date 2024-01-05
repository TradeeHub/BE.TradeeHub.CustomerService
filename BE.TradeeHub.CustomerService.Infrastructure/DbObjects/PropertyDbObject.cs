using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Infrastructure.DbObjects;

public class PropertyDbObject
{
    [BsonId]
    public ObjectId Id { get; set; }
    public AddressDbObject Property { get; set; } = null!;
    public AddressDbObject? Billing{ get; set; }
    public string? Location { get; set; }
    public List<ObjectId> Customers { get; set; } = null!;
    public List<ObjectId>? Quotes { get; set; }
    public List<ObjectId>? Jobs { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
}