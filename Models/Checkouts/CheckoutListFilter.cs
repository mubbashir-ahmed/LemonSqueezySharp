using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.Checkouts;

/// <summary>
/// Filter for <c>GET /v1/checkouts</c>.
/// </summary>
public sealed class CheckoutListFilter : ListFilter
{
    /// <summary>Only return checkouts for this store.</summary>
    public string? StoreId { get; set; }

    /// <summary>Only return checkouts for this variant.</summary>
    public string? VariantId { get; set; }
}
