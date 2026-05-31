using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.Discounts;

/// <summary>
/// Filter for <c>GET /v1/discounts</c>.
/// </summary>
public sealed class DiscountListFilter : ListFilter
{
    public string? StoreId { get; set; }
}
