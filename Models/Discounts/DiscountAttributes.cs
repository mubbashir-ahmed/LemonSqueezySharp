using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Discounts;

/// <summary>
/// Discount attribute payload.
/// See https://docs.lemonsqueezy.com/api/discounts.
/// </summary>
public sealed class DiscountAttributes
{
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    /// <summary>Either <c>"percent"</c> or <c>"fixed"</c>.</summary>
    [JsonPropertyName("amount_type")]
    public string? AmountType { get; set; }

    [JsonPropertyName("is_limited_to_products")]
    public bool? IsLimitedToProducts { get; set; }

    [JsonPropertyName("is_limited_redemptions")]
    public bool? IsLimitedRedemptions { get; set; }

    [JsonPropertyName("max_redemptions")]
    public int? MaxRedemptions { get; set; }

    [JsonPropertyName("starts_at")]
    public DateTimeOffset? StartsAt { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }

    /// <summary>One of <c>once</c>, <c>repeating</c>, <c>forever</c>.</summary>
    [JsonPropertyName("duration")]
    public string? Duration { get; set; }

    [JsonPropertyName("duration_in_months")]
    public int? DurationInMonths { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
