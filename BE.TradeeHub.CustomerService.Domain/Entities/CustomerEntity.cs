using BE.TradeeHub.CustomerService.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class CustomerEntity
{
    [BsonId]
    public ObjectId Id { get; set; }
    public Guid UserOwnerId { get; set; }
    public string CustomerReferenceNumber { get; set; }
    public string? Title { get; set; }
    public string? Name { get; set; }    
    public string? Surname { get; set; }
    public string FullName => $"{Title} {Name} {Surname}".Trim();
    public string? Alias { get; set; }
    public CustomerStatus Status { get; set; }
    public List<EmailEntity>? Emails { get; set; }
    public List<PhoneNumberEntity>? PhoneNumbers { get; set; }
    public List<ObjectId>? Properties { get; set; }
    public HashSet<string>? Tags { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public ObjectId? ReferredByCustomer { get; set; }
    public string? ReferredByOther { get; set; }
    public decimal? ReferralFeeFixed { get; set; }
    public decimal? ReferralFeePercentage { get; set; }
    public decimal? CustomerRating { get; set; }
    public List<ObjectId>? Comments { get; set; } 
}