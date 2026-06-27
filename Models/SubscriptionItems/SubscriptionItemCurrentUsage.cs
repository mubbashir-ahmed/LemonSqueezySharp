using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.SubscriptionItems;

/// <summary>
/// Response payload of <c>GET /v1/subscription-items/{id}/current-usage</c>. Returned
/// inside the JSON:API <c>meta</c> block — exposed here as a flat record for ergonomics.
/// </summary>
public sealed class SubscriptionItemCurrentUsage
{
    /// <summary>Start of the current billing period (<c>period_start</c>).</summary>
    [JsonPropertyName("period_start")]
    public DateTimeOffset? PeriodStart { get; set; }

    /// <summary>End of the current billing period (<c>period_end</c>).</summary>
    [JsonPropertyName("period_end")]
    public DateTimeOffset? PeriodEnd { get; set; }

    /// <summary>Total usage quantity recorded in the current period (<c>quantity</c>).</summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>Unit of the billing interval, e.g. <c>"month"</c> (<c>interval_unit</c>).</summary>
    [JsonPropertyName("interval_unit")]
    public string? IntervalUnit { get; set; }

    /// <summary>Number of interval units per billing period (<c>interval_quantity</c>).</summary>
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
