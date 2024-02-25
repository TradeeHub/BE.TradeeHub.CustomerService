using BE.TradeeHub.CustomerService.Domain.Interfaces;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;
using BE.TradeeHub.CustomerService.Domain.SubgraphEntities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Domain.Entities.Reference;

public class ExternalReferenceEntity : IOwnedEntity
{
    [BsonId]
    public ObjectId Id { get; set; }
    public Guid UserOwnerId { get; set; }
    public string ReferenceType { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool UseCompanyName { get; set; }
    public string? CompanyName { get; set; }
    public PhoneNumberEntity? PhoneNumber { get; set; }
    public EmailEntity? Email { get; set; }
    public string? Url { get; set; } = null!;
    public PlaceRequestEntity? Place { get; set; }
    public string? Description { get; set; } = null!;
    public CompensationDetailsEntity? Compensation  { get; set; }
    public UserEntity Owner() => new() { Id = UserOwnerId };
    
    public ExternalReferenceEntity()
    {
    }
    
    public ExternalReferenceEntity(IAddNewExternalReferenceRequest request, Guid userOwnerId)
    {
        UserOwnerId = userOwnerId;
        ReferenceType = request.ReferenceType;
        Name = request.Name;
        UseCompanyName = request.UseCompanyName;
        CompanyName = request.CompanyName;
        PhoneNumber = request.PhoneNumber != null ? new PhoneNumberEntity(request.PhoneNumber) : null;
        Email = request.Email != null ? new EmailEntity(request.Email) : null;
        Url = request.Url;
        Place = request.Place != null ? new PlaceRequestEntity(request.Place) : null;
        Description = request.Description;
        Compensation = request.Compensation != null ? new CompensationDetailsEntity(request.Compensation) : null;
    }
}