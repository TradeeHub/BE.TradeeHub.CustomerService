using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class PhoneNumberEntity
{
    public string PhoneNumberType { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public bool ReceiveNotifications { get; set; }
    
    public PhoneNumberEntity()
    {
    }
    
    public PhoneNumberEntity(IPhoneNumberRequest addPhoneNumberRequest)
    {
        PhoneNumber = addPhoneNumberRequest.PhoneNumber.Trim();
        PhoneNumberType = addPhoneNumberRequest.PhoneNumberType.Trim();
        ReceiveNotifications = addPhoneNumberRequest.ReceiveNotifications;
    }
}