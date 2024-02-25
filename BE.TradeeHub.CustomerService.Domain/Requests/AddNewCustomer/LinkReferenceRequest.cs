using BE.TradeeHub.CustomerService.Domain.Enums;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

public class LinkReferenceRequest
{
    public ObjectId Id { get; set; }
    public ReferenceType ReferenceType { get; set; }
}