using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Domain.Entities.Reference;

public class ExternalReferenceEntity
{
    public ObjectId Id { get; set; }
    public string ReferenceType { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? CompanyName { get; set; }
    public PhoneNumberEntity? PhoneNumber { get; set; }
    public EmailEntity? Email { get; set; }
    public string? Url { get; set; } = null!;
    public PlaceEntity? Place { get; set; }
    public string? Description { get; set; } = null!;
    public CompensationDetailsEntity? Compensation  { get; set; }
}