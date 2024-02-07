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
    public List<string> UploadUrls { get; set; }
    [BsonRepresentation(BsonType.String)]
    public CommentType CommentType { get; set; }

    public CommentEntity()
    {
        
    }
    public CommentEntity(Guid userOwnerId, string comment, Guid createdBy)
    {
        UserOwnerId = userOwnerId;
        Comment = comment.Trim();
        CreatedAt = DateTime.UtcNow;
        CreatedById = createdBy;
        CommentType = CommentType.General;
        UploadUrls = [];
        Archived = false;
    }
}
