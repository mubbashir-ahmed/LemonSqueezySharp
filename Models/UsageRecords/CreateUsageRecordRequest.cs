namespace LemonSqueezy.NET.Models.UsageRecords;

/// <summary>
/// Inputs for <c>POST /v1/usage-records</c>.
/// </summary>
public sealed class CreateUsageRecordRequest
{
    /// <summary>The metered subscription item this usage applies to. Required.</summary>
    public string SubscriptionItemId { get; set; } = string.Empty;

    /// <summary>Quantity of usage to record. Required.</summary>
    public int Quantity { get; set; }

    /// <summary>
    /// <c>"increment"</c> (default — add to current period total) or
    /// <c>"set"</c> (overwrite the current period total).
    /// </summary>
    public string? Action { get; set; }
}

/// <summary>
/// Wire-format attributes sent inside the JSON:API write envelope.
/// </summary>
internal sealed class UsageRecordCreateAttributes
{
    [System.Text.Json.Serialization.JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("action")]
    public string? Action { get; set; }
}
