using BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

public interface IPropertyRequest
{
    public PlaceRequest Property { get; set; }
    public bool IsBillingAddress { get; set; }
    public PlaceRequest? Billing{ get; set; }
}