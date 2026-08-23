using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.Disputes;

/// <summary>
/// Dispute attribute payload. Disputes are delivered via the <c>dispute_created</c> and
/// <c>dispute_resolved</c> webhooks.
/// See https://docs.lemonsqueezy.com/api/disputes.
/// </summary>
public sealed class DisputeAttributes
{
    /// <summary>Identifier of the store the dispute belongs to (<c>store_id</c>).</summary>
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    /// <summary>Identifier of the disputed order (<c>order_id</c>).</summary>
    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    /// <summary>Disputed amount, in cents (<c>amount</c>).</summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    /// <summary>Currency of the disputed amount (<c>currency</c>).</summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>Reason the dispute was raised (<c>reason</c>).</summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    /// <summary>Current status of the dispute (<c>status</c>).</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>Whether the dispute was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    /// <summary>Date and time the dispute was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the dispute was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
