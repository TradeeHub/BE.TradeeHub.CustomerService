using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Infrastructure.DbObjects;

public class CustomerDbObject
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string? Title { get; set; }
    public string? Name { get; set; }    
    public string? Surname { get; set; }
    public string? Alias { get; set; }
    public List<EmailDbObject>? Emails { get; set; }
    public List<PhoneNumberDbObject>? PhoneNumbers { get; set; }
    public List<ObjectId>? Properties { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public ObjectId? ReferredByCustomer { get; set; }
    public string? ReferredByOther { get; set; }
    public double? ReferralFeeFixed { get; set; }
    public double? ReferralFeePercentage { get; set; }
    public string? AdditionalNotes { get; set; } 
}