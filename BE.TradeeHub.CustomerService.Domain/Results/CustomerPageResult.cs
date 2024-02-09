using BE.TradeeHub.CustomerService.Domain.Entities;

namespace BE.TradeeHub.CustomerService.Domain.Results;

public class CustomerPageResult
{
    public List<CustomerEntity>? Customers { get; set; }
    public bool HasNextPage { get; set; }
    public string? NextCursor {get ; set;}
}