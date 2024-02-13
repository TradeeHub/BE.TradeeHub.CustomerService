using BE.TradeeHub.CustomerService.Domain.SubgraphEntities;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Types.Subgraph;

public class UserType : ObjectType<UserEntity>
{
    protected override void Configure(IObjectTypeDescriptor<UserEntity> descriptor)
    {
        descriptor.Field(u => u.Id).ID();
    }
}