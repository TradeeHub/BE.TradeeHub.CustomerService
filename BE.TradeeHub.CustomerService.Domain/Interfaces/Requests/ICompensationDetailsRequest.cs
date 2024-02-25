using BE.TradeeHub.CustomerService.Domain.Enums;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

public interface ICompensationDetailsRequest
{
    public CompensationType Type { get; }
    public decimal Amount { get; }
    public string? Currency { get; }
}