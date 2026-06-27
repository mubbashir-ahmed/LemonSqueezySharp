using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.Affiliates;

/// <summary>
/// Filter for <c>GET /v1/affiliates</c>.
/// </summary>
public sealed class AffiliateListFilter : ListFilter
{
    /// <summary>Only return affiliates belonging to this store.</summary>
    public string? StoreId { get; set; }
}
