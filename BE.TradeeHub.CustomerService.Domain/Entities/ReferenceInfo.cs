using BE.TradeeHub.CustomerService.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class ReferenceInfo
{
    public ObjectId? CustomerId { get; set; } // If the reference is another customer
    public ObjectId? ExternalReferenceId { get; set; } // If the reference is external
    [BsonRepresentation(BsonType.String)] 
    public ReferenceType ReferenceType { get; set; }
    
    public ReferenceInfo(ObjectId referenceId, ReferenceType referenceType)
    {
        CustomerId = referenceType == ReferenceType.Customer ? referenceId : null;
        ExternalReferenceId = referenceType == ReferenceType.External ? referenceId : null;
        ReferenceType = referenceType;
    }
}