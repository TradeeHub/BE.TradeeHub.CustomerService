namespace BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;

public class ViewportRequest
{
    public LocationRequest Northeast { get; set; }
    public LocationRequest Southwest { get; set; }
}