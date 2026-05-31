using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.SubscriptionItems;

/// <summary>
/// Response payload of <c>GET /v1/subscription-items/{id}/current-usage</c>. Returned
/// inside the JSON:API <c>meta</c> block — exposed here as a flat record for ergonomics.
/// </summary>
public sealed class SubscriptionItemCurrentUsage
{
    [JsonPropertyName("period_start")]
    public DateTimeOffset? PeriodStart { get; set; }

    [JsonPropertyName("period_end")]
    public DateTimeOffset? PeriodEnd { get; set; }

    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    [JsonPropertyName("interval_unit")]
    public string? IntervalUnit { get; set; }

    [JsonPropertyName("interval_quantity")]
    public int? IntervalQuantity { get; set; }
}

/// <summary>
/// Internal envelope shape — current-usage is delivered under <c>meta</c>, not <c>data</c>.
/// </summary>
internal sealed class SubscriptionItemCurrentUsageEnvelope
{
    [JsonPropertyName("meta")]
    public SubscriptionItemCurrentUsage? Meta { get; set; }
}
