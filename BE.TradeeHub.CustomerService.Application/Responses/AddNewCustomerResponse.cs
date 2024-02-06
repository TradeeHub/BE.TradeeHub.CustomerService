using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.Responses;

public class AddNewCustomerResponse
{
    public ObjectId Id { get; set; }
    public string CustomerReferenceNumber { get; set; } = null!;
}