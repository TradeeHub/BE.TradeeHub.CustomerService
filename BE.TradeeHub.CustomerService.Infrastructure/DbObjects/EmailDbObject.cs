    namespace BE.TradeeHub.CustomerService.Infrastructure.DbObjects;

    public class EmailDbObject
    {
        public string EmailType { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool ReceiveNotifications { get; set; }
    }