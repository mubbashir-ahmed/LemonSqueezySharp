using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Subscriptions;

/// <summary>
/// Subscription attribute payload. Starter set — extend as endpoints land.
/// See https://docs.lemonsqueezy.com/api/subscriptions.
/// </summary>
public sealed class SubscriptionAttributes
{
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    [JsonPropertyName("customer_id")]
    public long? CustomerId { get; set; }

    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    [JsonPropertyName("product_id")]
    public long? ProductId { get; set; }

    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    [JsonPropertyName("user_email")]
    public string? UserEmail { get; set; }

    [JsonPropertyName("status")]
    public SubscriptionStatus? Status { get; set; }

    [JsonPropertyName("cancelled")]
    public bool? Cancelled { get; set; }

    [JsonPropertyName("trial_ends_at")]
    public DateTimeOffset? TrialEndsAt { get; set; }

    [JsonPropertyName("renews_at")]
    public DateTimeOffset? RenewsAt { get; set; }

    [JsonPropertyName("ends_at")]
    public DateTimeOffset? EndsAt { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
