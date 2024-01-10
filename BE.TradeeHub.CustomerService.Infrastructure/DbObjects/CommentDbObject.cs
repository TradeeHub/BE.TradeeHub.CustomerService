namespace BE.TradeeHub.CustomerService.Infrastructure.DbObjects;

public class CommentDbObject
{
    public string? Comment { get; set; }
    public required List<string> UploadUrls  { get; set; }
}