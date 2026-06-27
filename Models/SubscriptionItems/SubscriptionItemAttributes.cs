using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.SubscriptionItems;

/// <summary>
/// Subscription-item attribute payload.
/// See https://docs.lemonsqueezy.com/api/subscription-items.
/// </summary>
public sealed class SubscriptionItemAttributes
{
    /// <summary>Id of the subscription this item belongs to (<c>subscription_id</c>).</summary>
    [JsonPropertyName("subscription_id")]
    public long? SubscriptionId { get; set; }

    /// <summary>Id of the price associated with the item (<c>price_id</c>).</summary>
    [JsonPropertyName("price_id")]
    public long? PriceId { get; set; }

    /// <summary>Quantity of the subscription item (<c>quantity</c>).</summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>Whether the item is metered/usage-based (<c>is_usage_based</c>).</summary>
    [JsonPropertyName("is_usage_based")]
    public bool? IsUsageBased { get; set; }

    /// <summary>Date and time the item was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the item was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
