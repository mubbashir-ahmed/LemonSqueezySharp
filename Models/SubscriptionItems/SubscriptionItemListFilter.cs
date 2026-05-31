using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.SubscriptionItems;

/// <summary>
/// Filter for <c>GET /v1/subscription-items</c>.
/// </summary>
public sealed class SubscriptionItemListFilter : ListFilter
{
    public string? SubscriptionId { get; set; }
    public string? PriceId { get; set; }
}
