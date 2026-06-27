namespace LemonSqueezy.NET.Models.Discounts;

/// <summary>
/// Inputs for <c>POST /v1/discounts</c>.
/// </summary>
public sealed class CreateDiscountRequest
{
    /// <summary>Internal name shown in the dashboard. Required.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Discount code customers enter at checkout. Required.</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Discount amount. When <see cref="AmountType"/> is <c>"percent"</c> this is
    /// a percentage in the range 1–100. When <c>"fixed"</c> this is a cent value.
    /// Required.
    /// </summary>
    public long Amount { get; set; }

    /// <summary>Either <c>"percent"</c> or <c>"fixed"</c>. Required.</summary>
    public string AmountType { get; set; } = "percent";

    /// <summary>Store the discount belongs to. Required.</summary>
    public string StoreId { get; set; } = string.Empty;

    /// <summary>
    /// One of <c>once</c>, <c>repeating</c>, <c>forever</c>. Defaults to <c>once</c>
    /// when omitted.
    /// </summary>
    public string? Duration { get; set; }

    /// <summary>How many months the discount is applied when <see cref="Duration"/> is <c>repeating</c>.</summary>
    public int? DurationInMonths { get; set; }

    /// <summary>Date and time the discount becomes active.</summary>
    public DateTimeOffset? StartsAt { get; set; }

    /// <summary>Date and time the discount expires.</summary>
    public DateTimeOffset? ExpiresAt { get; set; }

    /// <summary>Whether the discount is limited to a maximum number of redemptions.</summary>
    public bool? IsLimitedRedemptions { get; set; }

    /// <summary>Maximum number of redemptions allowed when <see cref="IsLimitedRedemptions"/> is <c>true</c>.</summary>
    public int? MaxRedemptions { get; set; }
}

/// <summary>
/// Wire-format attributes sent inside the JSON:API write envelope.
/// </summary>
internal sealed class DiscountCreateAttributes
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [System.Text.Json.Serialization.JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [System.Text.Json.Serialization.JsonPropertyName("amount")]
    public long Amount { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("amount_type")]
    public string AmountType { get; set; } = "percent";

    [System.Text.Json.Serialization.JsonPropertyName("duration")]
    public string? Duration { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("duration_in_months")]
    public int? DurationInMonths { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("starts_at")]
    public DateTimeOffset? StartsAt { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("is_limited_redemptions")]
    public bool? IsLimitedRedemptions { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("max_redemptions")]
    public int? MaxRedemptions { get; set; }
}
