using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

namespace BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

public class PhoneNumberRequest : IPhoneNumberRequest
{
    public string PhoneNumberType { get; set; }
    public string PhoneNumber { get; set; }
    public bool ReceiveNotifications { get; set; }
    
    PhoneNumberRequest()
    {
    } 
    
    public PhoneNumberRequest(string phoneNumber, string phoneNumberType, bool receiveNotifications)
    {
        PhoneNumber = phoneNumber;
        PhoneNumberType = phoneNumberType;
        ReceiveNotifications = receiveNotifications;
    }
}