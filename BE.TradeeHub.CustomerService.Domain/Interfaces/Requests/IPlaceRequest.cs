using BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

public interface IPlaceRequest
{
    public string PlaceId { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }
    public string CallingCode { get; set; }
    public LocationRequest Location { get; set; }
    public ViewportRequest Viewport { get; set; }
}