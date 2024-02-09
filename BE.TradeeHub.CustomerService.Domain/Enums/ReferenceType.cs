using System.ComponentModel;

namespace BE.TradeeHub.CustomerService.Domain.Enums;

public enum ReferenceType
{
    [Description("Customer")]
    Customer,
    [Description("External")]
    External,
}