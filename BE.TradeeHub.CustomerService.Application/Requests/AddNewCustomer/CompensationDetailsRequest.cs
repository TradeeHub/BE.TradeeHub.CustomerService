using BE.TradeeHub.CustomerService.Domain.Enums;

namespace BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;

public class CompensationDetailsRequest
{
    public CompensationType Type { get; set; }
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
}