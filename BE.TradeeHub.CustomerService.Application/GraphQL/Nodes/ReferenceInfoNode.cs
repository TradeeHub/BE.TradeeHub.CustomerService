using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Entities.Reference;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Nodes;

[ExtendObjectType(typeof(ReferenceInfoEntity))]
public class ReferenceInfoNode
{
    public static async Task<CustomerEntity?> GetCustomer([Parent] ReferenceInfoEntity referenceInfo,
        ICustomerGroupedByIdDataLoader customerGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (referenceInfo.CustomerId == null || referenceInfo.CustomerId == ObjectId.Empty)
        {
            return null;
        }

        var customers = await customerGroupedByIdDataLoader.LoadAsync(referenceInfo.CustomerId.Value, ctx);

        return customers.FirstOrDefault();
    }

    public static async Task<ExternalReferenceEntity?> GetExternalReference([Parent] ReferenceInfoEntity referenceInfo,
        IExternalReferenceGroupedByIdDataLoader externalReferenceGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (referenceInfo.ExternalReferenceId == null || referenceInfo.ExternalReferenceId == ObjectId.Empty)
        {
            return null;
        }

        var externalReferences = await externalReferenceGroupedByIdDataLoader.LoadAsync(referenceInfo.ExternalReferenceId.Value, ctx);

        return externalReferences.FirstOrDefault();
    }
}