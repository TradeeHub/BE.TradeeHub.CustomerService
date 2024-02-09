using BE.TradeeHub.CustomerService.Domain.Enums;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.Responses;

public class ReferenceResponse
{
    public ObjectId Id { get; set; }
    public string DisplayName { get; set; }
    public string? PhoneNumber { get; set; }
    public ReferenceType ReferenceType { get; set; }
}