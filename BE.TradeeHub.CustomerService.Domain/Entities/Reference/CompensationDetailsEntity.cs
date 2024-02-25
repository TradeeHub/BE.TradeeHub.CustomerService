using BE.TradeeHub.CustomerService.Domain.Enums;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

namespace BE.TradeeHub.CustomerService.Domain.Entities.Reference;

public class CompensationDetailsEntity
{
    public CompensationType CompensationType { get; set; } // Fixed, Percentage
    public decimal Amount { get; set; } // Could be fixed amount or percentage value
    public string? Currency { get; set; } // Useful for fixed amounts
    
    public CompensationDetailsEntity()
    {
    }
    
    public CompensationDetailsEntity(ICompensationDetailsRequest addRequest)
    {
        CompensationType = addRequest.Type;
        Amount = addRequest.Amount;
        Currency = addRequest.Currency;
    }
}