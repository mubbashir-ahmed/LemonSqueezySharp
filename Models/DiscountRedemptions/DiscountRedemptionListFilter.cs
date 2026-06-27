using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.DiscountRedemptions;

/// <summary>
/// Filter for <c>GET /v1/discount-redemptions</c>.
/// </summary>
public sealed class DiscountRedemptionListFilter : ListFilter
{
    /// <summary>Only return redemptions of this discount.</summary>
    public string? DiscountId { get; set; }

    /// <summary>Only return redemptions applied to this order.</summary>
    public string? OrderId { get; set; }
}
