using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.Responses;

public class AddNewUserResponse
{
    public ObjectId Id { get; set; }
    public string CustomerReferenceNumber { get; set; } = null!;
}