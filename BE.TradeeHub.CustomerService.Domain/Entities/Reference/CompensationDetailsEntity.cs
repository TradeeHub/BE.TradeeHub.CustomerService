using BE.TradeeHub.CustomerService.Domain.Enums;

namespace BE.TradeeHub.CustomerService.Domain.Entities.Reference;

public class CompensationDetailsEntity
{
    public CompensationType Type { get; set; } // Fixed, Percentage
    public decimal Amount { get; set; } // Could be fixed amount or percentage value
    public string? Currency { get; set; } // Useful for fixed amounts
}