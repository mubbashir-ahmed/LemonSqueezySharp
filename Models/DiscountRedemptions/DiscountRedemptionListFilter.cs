using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.DiscountRedemptions;

/// <summary>
/// Filter for <c>GET /v1/discount-redemptions</c>.
/// </summary>
public sealed class DiscountRedemptionListFilter : ListFilter
{
    public string? DiscountId { get; set; }
    public string? OrderId { get; set; }
}
