using BE.TradeeHub.CustomerService.Domain.Entities.Reference;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Types;

public class ExternalReferenceType : ObjectType<ExternalReferenceEntity>
{
    protected override void Configure(IObjectTypeDescriptor<ExternalReferenceEntity> descriptor)
    {
    }
}