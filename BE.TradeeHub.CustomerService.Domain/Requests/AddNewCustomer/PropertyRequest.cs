using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

namespace BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

public class PropertyRequest : IPropertyRequest
{
    public PlaceRequest Property { get; set; }
    public bool IsBillingAddress { get; set; }
    public PlaceRequest? Billing{ get; set; }
}