using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.Webhooks;

/// <summary>
/// Webhook-subscription attribute payload (server-side webhook management).
/// See https://docs.lemonsqueezy.com/api/webhooks.
/// </summary>
public sealed class WebhookAttributes
{
    /// <summary>Identifier of the store this webhook belongs to.</summary>
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

    /// <summary>When an event was last dispatched to this webhook, if ever.</summary>
    [JsonPropertyName("last_sent_at")]
    public DateTimeOffset? LastSentAt { get; set; }

    /// <summary>Whether this webhook was created in test mode.</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    /// <summary>When this webhook was created.</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>When this webhook was last updated.</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
