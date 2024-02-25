using BE.TradeeHub.CustomerService.Domain.Enums;
using BE.TradeeHub.CustomerService.Domain.Interfaces;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class CustomerEntity : AuditableEntity, IOwnedEntity
{
    [BsonId] 
    public ObjectId Id { get; set; }
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
    public List<ObjectId>? PropertyIds { get; set; }
    public HashSet<string>? Tags { get; set; }
    public ReferenceInfoEntity? Reference { get; set; }
    public decimal? CustomerRating { get; set; }
    public bool Archived { get; set; }
    public List<ObjectId>? CommentIds { get; set; }

    public CustomerEntity()
    {
    }
    
    public CustomerEntity(IAddNewCustomerRequest addRequest, IUserContext userContext)
    {
        UserOwnerId = userContext.UserId;
        CustomerType = addRequest.CustomerType.Trim() == "Empty" ? "" : addRequest.CustomerType.Trim();
        CompanyName = addRequest.CompanyName?.Trim();
        UseCompanyName = addRequest.UseCompanyName;
        Title = addRequest.Title?.Trim();
        Name = addRequest.Name?.Trim();
        Surname = addRequest.Surname?.Trim();
        Alias = addRequest.Alias?.Trim();
        Status = CustomerStatus.Lead;
        Tags = addRequest.Tags != null ? [..addRequest.Tags.Select(tag => tag.Trim())] : [];
        CreatedAt = DateTime.UtcNow;
        CreatedById = userContext.UserId;
        Reference = addRequest.Reference != null
            ? new ReferenceInfoEntity(addRequest.Reference.Id, addRequest.Reference.ReferenceType)
            : null;
        
        // Process Emails
        var emailList = addRequest.Emails?
            .Where(e => !string.IsNullOrWhiteSpace(e.Email))
            .Select(e => new EmailEntity(e))
            .ToList();
        
        Emails = emailList != null && emailList.Any() ? emailList : null;

        // Process PhoneNumbers
        var phoneNumberList = addRequest.PhoneNumbers?
            .Where(p => !string.IsNullOrWhiteSpace(p.PhoneNumber))
            .Select(p => new PhoneNumberEntity(p))
            .ToList();
        
        PhoneNumbers = phoneNumberList != null && phoneNumberList.Any() ? phoneNumberList : null;

        PropertyIds = new List<ObjectId>();
        CommentIds = new List<ObjectId>();
        FullName = $"{Title?.Trim()} {Name?.Trim()} {Surname?.Trim()}".Trim(); // This will trim the FullName as well
    }
}