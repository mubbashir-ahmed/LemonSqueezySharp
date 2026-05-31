using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.Prices;

/// <summary>
/// Filter for <c>GET /v1/prices</c>.
/// </summary>
public sealed class PriceListFilter : ListFilter
{
    /// <summary>
    /// Filter prices by variant id (<c>filter[variant_id]</c>).
    /// </summary>
    public string? VariantId { get; set; }
}
