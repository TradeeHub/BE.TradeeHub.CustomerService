using BE.TradeeHub.CustomerService.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class CustomerEntity
{
    [BsonId] public ObjectId Id { get; set; }
    public Guid UserOwnerId { get; set; }
    public string CustomerType { get; set; } = null!;
    public string? CompanyName { get; set; }
    public bool UseCompanyName { get; set; }
    public string? CustomerReferenceNumber { get; set; }
    public string? Title { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }

    public string? FullName { get; private set; }
    public string? Alias { get; set; }
    [BsonRepresentation(BsonType.String)] public CustomerStatus Status { get; set; }
    public List<EmailEntity>? Emails { get; set; }
    public List<PhoneNumberEntity>? PhoneNumbers { get; set; }
    public List<ObjectId>? Properties { get; set; }
    public HashSet<string>? Tags { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public ReferenceInfo? Reference{ get; set; }
    public decimal? CustomerRating { get; set; }
    public bool Archived { get; set; }
    public List<ObjectId>? Comments { get; set; }

    public CustomerEntity(Guid userOwnerId, string? title, string? name, string? surname, string? alias, string customerType, string? companyName, bool useCompanyName, ObjectId? referenceId, ReferenceType? referenceType,
        IEnumerable<string>? tags, Guid createdBy, IEnumerable<EmailEntity>? emails,
        IEnumerable<PhoneNumberEntity>? phoneNumbers)
    {
        UserOwnerId = userOwnerId;
        CustomerType = customerType.Trim();
        CompanyName = companyName?.Trim();
        UseCompanyName = useCompanyName;
        Title = title?.Trim();
        Name = name?.Trim();
        Surname = surname?.Trim();
        Alias = alias?.Trim();
        Status = CustomerStatus.Lead;
        Tags = tags != null ? [..tags.Select(tag => tag.Trim())] : [];
        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
        Reference = referenceId != null && referenceType != null ? new ReferenceInfo(referenceId.Value, referenceType.Value) : null;
        Emails = emails?.Select(e => new EmailEntity(e.Email.Trim(), e.EmailType.Trim(), e.ReceiveNotifications))
            .ToList();
        PhoneNumbers = phoneNumbers?.Select(p =>
            new PhoneNumberEntity(p.PhoneNumber.Trim(), p.PhoneNumberType.Trim(), p.ReceiveNotifications)).ToList();
        Properties = new List<ObjectId>();
        Comments = new List<ObjectId>();
        FullName = $"{Title?.Trim()} {Name?.Trim()} {Surname?.Trim()}".Trim(); // This will trim the FullName as well
    }
}