using System.ComponentModel;

namespace BE.TradeeHub.CustomerService.Domain.Enums;

public enum CompensationType
{
    [Description("Fixed one time only for first job done for customer")]
    OneTimeFixed,
    [Description("Fixed for every job received from customer")]
    RecurringFixed,
    [Description("Percentage for the first job done for customer only")]
    OneTimePercentage,
    [Description("Percentage for every job received from customer")]
    RecurringPercentage,
    [Description("Weekly")]
    Weekly,
    [Description("Monthly")]
    Monthly,
    [Description("Yearly")]
    Yearly
}