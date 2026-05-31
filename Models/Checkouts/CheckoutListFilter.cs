using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.Checkouts;

/// <summary>
/// Filter for <c>GET /v1/checkouts</c>.
/// </summary>
public sealed class CheckoutListFilter : ListFilter
{
    public string? StoreId { get; set; }
    public string? VariantId { get; set; }
}
