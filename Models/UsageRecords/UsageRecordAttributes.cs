using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.UsageRecords;

/// <summary>
/// Usage-record attribute payload.
/// See https://docs.lemonsqueezy.com/api/usage-records.
/// </summary>
public sealed class UsageRecordAttributes
{
    [JsonPropertyName("subscription_item_id")]
    public long? SubscriptionItemId { get; set; }

    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>
    /// One of <c>increment</c> (default — add to current period total) or
    /// <c>set</c> (overwrite the current period total).
    /// </summary>
    [JsonPropertyName("action")]
    public string? Action { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
