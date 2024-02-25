namespace BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

public class ViewportRequest
{
    public LocationRequest Northeast { get; set; }
    public LocationRequest Southwest { get; set; }
}