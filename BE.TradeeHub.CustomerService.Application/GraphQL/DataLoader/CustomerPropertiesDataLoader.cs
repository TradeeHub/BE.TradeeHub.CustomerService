using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.DataLoader;

public class CustomerPropertiesDataLoader : GroupedDataLoader<ObjectId, PropertyEntity>
{
    private readonly IPropertyRepository _propertyRepository;

    public CustomerPropertiesDataLoader(IBatchScheduler batchScheduler, IPropertyRepository propertyRepository, DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _propertyRepository = propertyRepository;
    }

    protected override async Task<ILookup<ObjectId, PropertyEntity>> LoadGroupedBatchAsync(IReadOnlyList<ObjectId> customerIds, CancellationToken cancellationToken)
    {
        // Fetch all properties that are related to the provided customer IDs
        var properties = await _propertyRepository.GetPropertiesByCustomerIds(customerIds, cancellationToken);

        // Group the properties by each customer ID
        return customerIds
            .SelectMany(customerId => 
                properties.Where(p => p.Customers.Contains(customerId))
                    .Select(p => new { CustomerId = customerId, Property = p }))
            .ToLookup(x => x.CustomerId, x => x.Property);
    }
}