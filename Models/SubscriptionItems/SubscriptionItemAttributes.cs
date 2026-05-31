using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.SubscriptionItems;

/// <summary>
/// Subscription-item attribute payload.
/// See https://docs.lemonsqueezy.com/api/subscription-items.
/// </summary>
public sealed class SubscriptionItemAttributes
{
    [JsonPropertyName("subscription_id")]
    public long? SubscriptionId { get; set; }

    [JsonPropertyName("price_id")]
    public long? PriceId { get; set; }

    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    [JsonPropertyName("is_usage_based")]
    public bool? IsUsageBased { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
