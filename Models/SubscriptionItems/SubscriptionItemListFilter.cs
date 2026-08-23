using LemonSqueezySharp.Models.Requests;

namespace LemonSqueezySharp.Models.SubscriptionItems;

/// <summary>
/// Filter for <c>GET /v1/subscription-items</c>.
/// </summary>
public sealed class SubscriptionItemListFilter : ListFilter
{
    /// <summary>
    /// Filter subscription items by subscription id (<c>filter[subscription_id]</c>).
    /// </summary>
    public string? SubscriptionId { get; set; }

    /// <summary>
    /// Filter subscription items by price id (<c>filter[price_id]</c>).
    /// </summary>
    public string? PriceId { get; set; }
}
