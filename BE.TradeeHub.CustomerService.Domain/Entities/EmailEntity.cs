    namespace BE.TradeeHub.CustomerService.Domain.Entities;

    public class EmailEntity
    {
        public string EmailType { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool ReceiveNotifications { get; set; }
    }