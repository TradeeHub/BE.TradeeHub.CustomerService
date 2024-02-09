namespace BE.TradeeHub.CustomerService.Application.Requests;

public class SearchReferenceRequest
{
    public string SearchTerm { get; set; }
    public int PageSize { get; set; }   
    public bool? CustomerHasNextPage { get; set; }
    public bool? ExternalHasNextPage { get; set; }
    public string? CustomerNextCursor { get; set; }
    public string? ExternalNextCursor { get; set; }
}