using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Disputes;

/// <summary>
/// Dispute attribute payload. Disputes are delivered via the <c>dispute_created</c> and
/// <c>dispute_resolved</c> webhooks.
/// See https://docs.lemonsqueezy.com/api/disputes.
/// </summary>
public sealed class DisputeAttributes
{
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
