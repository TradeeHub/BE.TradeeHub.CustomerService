namespace BE.TradeeHub.CustomerService.Domain.Interfaces;

public interface IAppSettings
{
    public string Environment { get; }
    public string MongoDbConnectionString { get; }
    public string MongoDbDatabaseName { get; }
    public string AppClientId { get; }
}