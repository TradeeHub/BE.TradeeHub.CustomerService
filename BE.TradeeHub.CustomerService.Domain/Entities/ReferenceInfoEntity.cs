using BE.TradeeHub.CustomerService.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class ReferenceInfoEntity
{
    public ObjectId? Customer { get; set; } // If the reference is another customer
    public ObjectId? ExternalReference { get; set; } // If the reference is external
    [BsonRepresentation(BsonType.String)] 
    public ReferenceType ReferenceType { get; set; }
    
    public ReferenceInfoEntity()
    {
    }
    public ReferenceInfoEntity(ObjectId referenceId, ReferenceType referenceType)
    {
        Customer = referenceType == ReferenceType.Customer ? referenceId : null;
        ExternalReference = referenceType == ReferenceType.External ? referenceId : null;
        ReferenceType = referenceType;
    }
}