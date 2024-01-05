using System.Diagnostics;
using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Infrastructure.Repositories;

public class CustomerRepository
{
    private readonly MongoDbContext _dbContext;

    public CustomerRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<CustomerDbObject> GetCustomerById(ObjectId customerId)
    {
        var filter = Builders<CustomerDbObject>.Filter.Eq(c => c.Id, customerId);
        var customer =  await _dbContext.Customers.Find(filter).FirstOrDefaultAsync();
        return customer;
    }
    
    public async Task<IEnumerable<CustomerDbObject>> GetAllCustomers()
    {
        return await _dbContext.Customers.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<CustomerDbObject>> GetCustomersByPropertyId(ObjectId propertyId)
    {
        var filter = Builders<CustomerDbObject>.Filter.AnyEq(c => c.Properties, propertyId);
        return await _dbContext.Customers.Find(filter).ToListAsync();
    }
    
    public async Task<IEnumerable<CustomerDbObject>> GetCustomersByName(string name)
    {
        var filter = Builders<CustomerDbObject>.Filter.Eq(c => c.Name, name);
        return await _dbContext.Customers.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<CustomerDbObject>> GetCustomersByAddress(string address)
    {
        // Find properties with the given address
        var propertyFilter = Builders<PropertyDbObject>.Filter.Eq(p => p.Property.Address, address);
        var properties = await _dbContext.Properties.Find(propertyFilter).ToListAsync();
    
        // Extract property IDs
        var propertyIds = properties.Select(p => p.Id);
    
        // Find customers linked to these properties
        var customerFilter = Builders<CustomerDbObject>.Filter.AnyIn(c => c.Properties, propertyIds);
        return await _dbContext.Customers.Find(customerFilter).ToListAsync();
    }
    
    public async Task<IEnumerable<CustomerDbObject>> GetCustomersByIds(IEnumerable<ObjectId> customerIds)
    {
        // Build a filter to select customers with IDs that are in the customerIds list
        var filter = Builders<CustomerDbObject>.Filter.In(c => c.Id, customerIds);

        // Execute the query using the Customers collection from MongoDbContext
        var cursor =  await _dbContext.Customers.FindAsync(filter);
        
        return await cursor.ToListAsync();
    }
    
    public async Task GenerateFakeData(int quantity, CancellationToken ctx)
    {
        var sw1 = new Stopwatch();
        var sw2 = new Stopwatch();
        
        sw1.Start();
        var fakeCustomers = FakeDataGenerator.CreateFakeCustomers(quantity);
        var random = new Random();
    
        var fakeProperties = fakeCustomers.AsParallel().SelectMany(customer => 
        {
            var randomQuantity = random.Next(1, 5);
            var customerFakeProperties = FakeDataGenerator.CreateFakeProperties(randomQuantity);
    
            customer.Properties = customerFakeProperties.Select(x => x.Id).ToList();
            customerFakeProperties.ForEach(fakeProp => fakeProp.Customers = [customer.Id]);
    
            return customerFakeProperties;
        }).ToList();
        
        sw1.Stop();
        sw2.Start();
        await _dbContext.Customers.InsertManyAsync(fakeCustomers, cancellationToken: ctx);
        await _dbContext.Properties.InsertManyAsync(fakeProperties, cancellationToken: ctx);
        sw2.Stop();
        Console.WriteLine($"Generating Fake Data Time: {sw1.Elapsed}, Saving Fake Data Time: {sw2.Elapsed}, Fake Customers Created: {fakeCustomers.Count}, Fake Properties Created: {fakeProperties.Count}");
    }
}