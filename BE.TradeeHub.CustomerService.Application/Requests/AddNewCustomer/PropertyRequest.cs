namespace BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;

public class PropertyRequest
{
    public PlaceRequest? Property { get; set; }
    public bool IsBillingAddress { get; set; }
    public PlaceRequest? Billing{ get; set; }
}