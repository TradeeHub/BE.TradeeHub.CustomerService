namespace BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;

public class EmailRequest
{
    public string EmailType { get; set; }
    public string Email { get; set; }
    public bool ReceiveNotifications { get; set; }
}