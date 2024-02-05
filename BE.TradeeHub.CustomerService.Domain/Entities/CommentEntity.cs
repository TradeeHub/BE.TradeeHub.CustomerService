using BE.TradeeHub.CustomerService.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class CommentEntity
{
    [BsonId]
    public ObjectId Id { get; set; }
    public ObjectId CustomerId { get; set; }
    public Guid UserOwnerId { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Archived {get; set; }
    public string? Comment { get; set; }
    public List<string> UploadUrls { get; set; } = new List<string>();
    public string CommentType { get; set; }
}
