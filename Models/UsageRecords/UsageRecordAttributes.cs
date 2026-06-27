using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.UsageRecords;

/// <summary>
/// Usage-record attribute payload.
/// See https://docs.lemonsqueezy.com/api/usage-records.
/// </summary>
public sealed class UsageRecordAttributes
{
    /// <summary>Id of the subscription item this usage applies to (<c>subscription_item_id</c>).</summary>
    [JsonPropertyName("subscription_item_id")]
    public long? SubscriptionItemId { get; set; }

    /// <summary>Quantity of usage recorded (<c>quantity</c>).</summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>
    /// One of <c>increment</c> (default — add to current period total) or
    /// <c>set</c> (overwrite the current period total).
    /// </summary>
    [JsonPropertyName("action")]
    public string? Action { get; set; }

    /// <summary>Date and time the usage record was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the usage record was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
