namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

public interface IPhoneNumberRequest
{
    public string PhoneNumberType { get; set; }
    public string PhoneNumber { get; set; }
    public bool ReceiveNotifications { get; set; }
}