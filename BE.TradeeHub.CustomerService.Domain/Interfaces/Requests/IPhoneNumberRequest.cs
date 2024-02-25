namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

public interface IPhoneNumberRequest
{
    public string PhoneNumberType { get; }
    public string PhoneNumber { get; }
    public bool ReceiveNotifications { get; }
}