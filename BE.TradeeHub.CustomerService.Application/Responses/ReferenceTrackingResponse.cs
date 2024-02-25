namespace BE.TradeeHub.CustomerService.Application.Responses;

public class ReferenceTrackingResponse
{
    public IEnumerable<ReferenceResponse> References { get; set; } = [];
    public bool CustomerHasNextPage { get; set; }
    public bool ExternalHasNextPage { get; set; }
    public string? CustomerNextCursor { get; set; }
    public string? ExternalNextCursor { get; set; }
}