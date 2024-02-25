using BE.TradeeHub.CustomerService.Domain.Enums;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

namespace BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

public class CompensationDetailsRequest : ICompensationDetailsRequest
{
    public CompensationType Type { get; set; }
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
}