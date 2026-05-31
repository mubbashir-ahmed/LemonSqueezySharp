using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.Variants;

/// <summary>
/// Filter for <c>GET /v1/variants</c>.
/// </summary>
public sealed class VariantListFilter : ListFilter
{
    /// <summary>
    /// Filter variants by the parent product id (<c>filter[product_id]</c>).
    /// </summary>
    public string? ProductId { get; set; }

    /// <summary>
    /// Filter variants by status (<c>filter[status]</c>) — e.g. <c>"published"</c>, <c>"draft"</c>.
    /// </summary>
    public string? Status { get; set; }
}
