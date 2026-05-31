using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Webhooks;

/// <summary>
/// Webhook-subscription attribute payload (server-side webhook management).
/// See https://docs.lemonsqueezy.com/api/webhooks.
/// </summary>
public sealed class WebhookAttributes
{
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    /// <summary>Endpoint LemonSqueezy will POST events to.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// LemonSqueezy event names this webhook subscribes to, e.g.
    /// <c>order_created</c>, <c>subscription_updated</c>.
    /// </summary>
    [JsonPropertyName("events")]
    public IReadOnlyList<string>? Events { get; set; }

    [JsonPropertyName("last_sent_at")]
    public DateTimeOffset? LastSentAt { get; set; }

    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
