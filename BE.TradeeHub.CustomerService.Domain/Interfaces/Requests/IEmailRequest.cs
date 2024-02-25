namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

public interface IEmailRequest
{
    public string EmailType { get; }
    public string Email { get; }
    public bool ReceiveNotifications { get; }
}