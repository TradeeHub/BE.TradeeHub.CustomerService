using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly MongoDbContext _dbContext;

    public CustomerRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CustomerEntity> GetCustomerByIdAsync(ObjectId customerId)
    {
        var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Id, customerId);
        var customer = await _dbContext.Customers.Find(filter).FirstOrDefaultAsync();
        return customer;
    }

    public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
    {
        return await _dbContext.Customers.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<CustomerEntity>> GetCustomersByPropertyIdAsync(ObjectId propertyId)
    {
        var filter = Builders<CustomerEntity>.Filter.AnyEq(c => c.Properties, propertyId);
        return await _dbContext.Customers.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<CustomerEntity>> GetCustomersByNameAsync(string name)
    {
        var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Name, name);
        return await _dbContext.Customers.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<CustomerEntity>> GetCustomersByAddressAsync(string address)
    {
        // Find properties with the given address
        var propertyFilter = Builders<PropertyEntity>.Filter.Eq(p => p.Property.Address, address);
        var properties = await _dbContext.Properties.Find(propertyFilter).ToListAsync();

        // Extract property IDs
        var propertyIds = properties.Select(p => p.Id);

        // Find customers linked to these properties
        var customerFilter = Builders<CustomerEntity>.Filter.AnyIn(c => c.Properties, propertyIds);
        return await _dbContext.Customers.Find(customerFilter).ToListAsync();
    }

    public async Task<IEnumerable<CustomerEntity>> GetCustomersByIdsAsync(IEnumerable<ObjectId> customerIds)
    {
        // Build a filter to select customers with IDs that are in the customerIds list
        var filter = Builders<CustomerEntity>.Filter.In(c => c.Id, customerIds);

        // Execute the query using the Customers collection from MongoDbContext
        var cursor = await _dbContext.Customers.FindAsync(filter);

        return await cursor.ToListAsync();
    }

    public async Task<(ObjectId Id, string CustomerReferenceNumber)> AddNewCustomerAsync(CustomerEntity customer,
        IList<PropertyEntity> properties,
        IList<CommentEntity> comments, CancellationToken ctx)
    {
        using var session = await _dbContext.Client.StartSessionAsync(cancellationToken: ctx);

        session.StartTransaction();

        try
        {
            // Generate unique CRN for the customer
            customer.CustomerReferenceNumber =
                await GenerateUniqueCustomerReferenceNumber(customer.UserOwnerId, session, ctx);

            // Initialize Properties and Comments lists if necessary
            customer.Properties = new List<ObjectId>();
            customer.Comments = new List<ObjectId>();

            // Add customer first to get its ID
            await _dbContext.Customers.InsertOneAsync(session, customer, cancellationToken: ctx);

            // Handle Properties in a single operation if there are any
            if(properties.Any())
            {
                foreach (var property in properties)
                {
                    property.Customers = [customer.Id];
                }

                await _dbContext.Properties.InsertManyAsync(session, properties, cancellationToken: ctx);
                customer.Properties.AddRange(properties.Select(p => p.Id));
            } 
            
            // Handle Comments in a single operation if there are any, making sure to link them to the customer
            if (comments.Any())
            {
                foreach (var comment in comments)
                {
                    comment.CustomerId = customer.Id;
                }

                await _dbContext.Comments.InsertManyAsync(session, comments, cancellationToken: ctx);
                customer.Comments.AddRange(comments.Select(c => c.Id));
            }

            // Update the customer with the new lists of property and comment IDs if any were added
            if (customer.Properties?.Any() == true || customer.Comments?.Any() == true)
            {
                var updateDefinition = Builders<CustomerEntity>.Update
                    .Set(c => c.Properties, customer.Properties)
                    .Set(c => c.Comments, customer.Comments);
                await _dbContext.Customers.UpdateOneAsync(session, c => c.Id == customer.Id, updateDefinition,
                    cancellationToken: ctx);
            }

            await session.CommitTransactionAsync(ctx);

            return (customer.Id, customer.CustomerReferenceNumber);
        }
        catch (Exception ex)
        {
            await session.AbortTransactionAsync(ctx);
            throw;
        }
    }


    private async Task<string> GenerateUniqueCustomerReferenceNumber(Guid userOwnerId, IClientSessionHandle session,
        CancellationToken ctx)
    {
        var customerRefNumCollection = _dbContext.CustomerReferenceNumber;

        // Define the filter to find the document for the specific UserOwnerId
        var filter = Builders<CustomerReferenceNumberEntity>.Filter.Eq(doc => doc.UserOwnerId, userOwnerId);

        // Define the update operation to increment the counter
        var update = Builders<CustomerReferenceNumberEntity>.Update.Inc(doc => doc.Counter, 1);

        // Specify that the operation should return the document after the update and create a new document if one doesn't exist
        var options = new FindOneAndUpdateOptions<CustomerReferenceNumberEntity, CustomerReferenceNumberEntity>
        {
            ReturnDocument = ReturnDocument.After,
            IsUpsert = true
        };

        // Perform the find and update operation atomically within the session
        var result = await customerRefNumCollection.FindOneAndUpdateAsync(session, filter, update, options, ctx);

        // If the result is null, explicitly insert a new document with Counter set to 1.
        // This case might occur if the FindOneAndUpdateAsync doesn't automatically upsert in certain conditions.
        if (result == null)
        {
            var newEntry = new CustomerReferenceNumberEntity
            {
                UserOwnerId = userOwnerId,
                Counter = 1
            };
            await customerRefNumCollection.InsertOneAsync(session, newEntry, cancellationToken: ctx);
            return "CRN-1";
        }

        // Construct the CRN using the updated counter value
        return $"CRN-{result.Counter}";
    }
}