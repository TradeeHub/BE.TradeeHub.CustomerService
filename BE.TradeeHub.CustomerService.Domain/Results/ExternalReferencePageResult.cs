using BE.TradeeHub.CustomerService.Domain.Entities.Reference;

namespace BE.TradeeHub.CustomerService.Domain.Results;

public class ExternalReferencePageResult
{
    public List<ExternalReferenceEntity>? ExternalReferences { get; set; }
    public bool HasNextPage { get; set; }
    public string? NextCursor { get; set; }
}