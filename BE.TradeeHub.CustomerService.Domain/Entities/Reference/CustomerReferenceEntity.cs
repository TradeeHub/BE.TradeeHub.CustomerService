using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Domain.Entities.Reference;

public class CustomerReferenceEntity
{
    public ObjectId Id { get; set; }
    public ObjectId CustomerId { get; set; }
    public ObjectId? ReferringCustomerId { get; set; }
    public ObjectId? ExternalReferenceId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}