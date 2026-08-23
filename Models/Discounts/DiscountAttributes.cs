using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.Discounts;

/// <summary>
/// Discount attribute payload.
/// See https://docs.lemonsqueezy.com/api/discounts.
/// </summary>
public sealed class DiscountAttributes
{
    /// <summary>Identifier of the store the discount belongs to (<c>store_id</c>).</summary>
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    /// <summary>Internal name of the discount (<c>name</c>).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>Discount code customers enter at checkout (<c>code</c>).</summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>Discount amount; a percentage or a cent value depending on <see cref="AmountType"/> (<c>amount</c>).</summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    /// <summary>Either <c>"percent"</c> or <c>"fixed"</c>.</summary>
    [JsonPropertyName("amount_type")]
    public string? AmountType { get; set; }

    /// <summary>Whether the discount is restricted to specific products (<c>is_limited_to_products</c>).</summary>
    [JsonPropertyName("is_limited_to_products")]
    public bool? IsLimitedToProducts { get; set; }

    /// <summary>Whether the discount is limited to a maximum number of redemptions (<c>is_limited_redemptions</c>).</summary>
    [JsonPropertyName("is_limited_redemptions")]
    public bool? IsLimitedRedemptions { get; set; }

    /// <summary>Maximum number of redemptions allowed (<c>max_redemptions</c>).</summary>
    [JsonPropertyName("max_redemptions")]
    public int? MaxRedemptions { get; set; }

    /// <summary>Date and time the discount becomes active (<c>starts_at</c>).</summary>
    [JsonPropertyName("starts_at")]
    public DateTimeOffset? StartsAt { get; set; }

    /// <summary>Date and time the discount expires (<c>expires_at</c>).</summary>
    [JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }

    /// <summary>One of <c>once</c>, <c>repeating</c>, <c>forever</c>.</summary>
    [JsonPropertyName("duration")]
    public string? Duration { get; set; }

    /// <summary>Number of months the discount is applied when <see cref="Duration"/> is <c>repeating</c> (<c>duration_in_months</c>).</summary>
    [JsonPropertyName("duration_in_months")]
    public int? DurationInMonths { get; set; }

    /// <summary>Current status of the discount (<c>status</c>).</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>Human-readable form of <see cref="Status"/> (<c>status_formatted</c>).</summary>
    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    /// <summary>Whether the discount was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    /// <summary>Date and time the discount was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the discount was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
