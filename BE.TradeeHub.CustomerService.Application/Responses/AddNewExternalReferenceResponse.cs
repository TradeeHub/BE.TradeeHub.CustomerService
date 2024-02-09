using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.Responses;

public class AddNewExternalReferenceResponse
{
    public ObjectId Id { get; set; }
    public string Name { get; set; } = null!;
}