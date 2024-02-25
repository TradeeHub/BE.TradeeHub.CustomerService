using BE.TradeeHub.CustomerService.Domain.Enums;
using BE.TradeeHub.CustomerService.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class CommentEntity : AuditableEntity, IOwnedEntity
{
    [BsonId]
    public ObjectId Id { get; set; }
    public ObjectId CustomerId { get; set; }
    public bool Archived {get; set; }
    public string? Comment { get; set; }
    public List<UploadEntity>? Uploads { get; set; }
    [BsonRepresentation(BsonType.String)]
    public CommentType CommentType { get; set; }

    public CommentEntity()
    {
        
    }
    public CommentEntity(string comment, IUserContext  userContext)
    {
        UserOwnerId = userContext.UserId;
        Comment = comment.Trim();
        CreatedAt = DateTime.UtcNow;
        CreatedById = userContext.UserId;
        CommentType = CommentType.General;
        Uploads = new List<UploadEntity>();
        Archived = false;
    }
}
