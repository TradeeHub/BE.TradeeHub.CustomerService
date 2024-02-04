namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class PhoneNumberEntity
{
    public string PhoneNumberType { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public bool ReceiveNotifications { get; set; }
}