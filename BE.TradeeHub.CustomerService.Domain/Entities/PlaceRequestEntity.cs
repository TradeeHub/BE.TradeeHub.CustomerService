using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;
using BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

namespace BE.TradeeHub.CustomerService.Domain.Entities;

public class PlaceRequestEntity : IPlaceRequest
{
    public string PlaceId { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }
    public string CallingCode { get; set; }
    public LocationRequest Location { get; set; }
    public ViewportRequest Viewport { get; set; }
    
    public PlaceRequestEntity(IPlaceRequest placeRequest)
    {
        PlaceId = placeRequest.PlaceId;
        Address = placeRequest.Address;
        Country = placeRequest.Country;
        CountryCode = placeRequest.CountryCode;
        CallingCode = placeRequest.CallingCode;
        Location = placeRequest.Location;
        Viewport = placeRequest.Viewport;
    }
}