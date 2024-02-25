using BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

public interface IPlaceRequest
{
    public string PlaceId { get; }
    public string Address { get; }
    public string Country { get; }
    public string CountryCode { get; }
    public string CallingCode { get; }
    public LocationRequest Location { get; }
    public ViewportRequest Viewport { get; }
}