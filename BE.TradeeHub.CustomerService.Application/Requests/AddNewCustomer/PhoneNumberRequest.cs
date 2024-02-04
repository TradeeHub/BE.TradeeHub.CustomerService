namespace BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;

public class PhoneNumberRequest
{
    public string PhoneNumberType { get; set; }
    public string PhoneNumber { get; set; }
    public bool ReceiveNotifications { get; set; }
}