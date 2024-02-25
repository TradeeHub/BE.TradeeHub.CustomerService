using BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

public interface IPropertyRequest
{
    public PlaceRequest Property { get; }
    public bool IsBillingAddress { get; }
    public PlaceRequest? Billing{ get; }
}