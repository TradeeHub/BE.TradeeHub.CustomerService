using BE.TradeeHub.CustomerService.Domain.Interfaces;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class PropertyEntity : AuditableEntity, IOwnedEntity
{
    [BsonId] public ObjectId Id { get; set; }
    public PlaceRequestEntity Property { get; set; } = null!;
    public PlaceRequestEntity? Billing { get; set; }
    public List<ObjectId> CustomerIds { get; set; } = null!;
    public List<ObjectId>? QuoteIds { get; set; }
    public List<ObjectId>? JobIds { get; set; }

    public PropertyEntity()
    {
    }

    public PropertyEntity(IPropertyRequest addRequest, IUserContext userContext)
    {
        UserOwnerId = userContext.UserId;
        Property = new PlaceRequestEntity(addRequest.Property);
        Billing = addRequest is { IsBillingAddress: true, Property: not null , Billing: null }
            ? new PlaceRequestEntity(addRequest.Property)
            : addRequest.Billing != null
                ? new PlaceRequestEntity(addRequest.Billing)
                : null;
        CustomerIds = [];
        QuoteIds = [];
        JobIds = [];
        CreatedAt = DateTime.UtcNow;
        CreatedById = userContext.UserId;
    }
}