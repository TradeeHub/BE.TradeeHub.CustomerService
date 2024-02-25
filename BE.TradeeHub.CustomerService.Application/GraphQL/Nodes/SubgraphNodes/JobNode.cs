using BE.TradeeHub.CustomerService.Domain.SubgraphEntities;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Nodes.SubgraphNodes;

[Node]
[ExtendObjectType(typeof(JobEntity))]
public static class JobNode;