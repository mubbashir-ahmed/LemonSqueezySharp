using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Variants;

/// <summary>
/// Variant attribute payload.
/// See https://docs.lemonsqueezy.com/api/variants.
/// </summary>
public sealed class VariantAttributes
{
    /// <summary>Identifier of the product the variant belongs to (<c>product_id</c>).</summary>
    [JsonPropertyName("product_id")]
    public long? ProductId { get; set; }

    /// <summary>Variant name (<c>name</c>).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>URL-friendly variant slug (<c>slug</c>).</summary>
    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    /// <summary>Variant description, may contain HTML (<c>description</c>).</summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>Variant price, in cents (<c>price</c>).</summary>
    [JsonPropertyName("price")]
    public long? Price { get; set; }

    /// <summary>Whether the variant is a subscription (<c>is_subscription</c>).</summary>
    [JsonPropertyName("is_subscription")]
    public bool? IsSubscription { get; set; }

    /// <summary>Billing interval unit, e.g. <c>day</c>, <c>month</c>, <c>year</c> (<c>interval</c>).</summary>
    [JsonPropertyName("interval")]
    public string? Interval { get; set; }

    /// <summary>Number of intervals between billings (<c>interval_count</c>).</summary>
    [JsonPropertyName("interval_count")]
    public int? IntervalCount { get; set; }

    /// <summary>Whether the variant offers a free trial (<c>has_free_trial</c>).</summary>
    [JsonPropertyName("has_free_trial")]
    public bool? HasFreeTrial { get; set; }

    /// <summary>Free-trial interval unit (<c>trial_interval</c>).</summary>
    [JsonPropertyName("trial_interval")]
    public string? TrialInterval { get; set; }

    /// <summary>Number of trial intervals (<c>trial_interval_count</c>).</summary>
    [JsonPropertyName("trial_interval_count")]
    public int? TrialIntervalCount { get; set; }

    /// <summary>Whether the variant uses pay-what-you-want pricing (<c>pay_what_you_want</c>).</summary>
    [JsonPropertyName("pay_what_you_want")]
    public bool? PayWhatYouWant { get; set; }

    /// <summary>Minimum pay-what-you-want price, in cents (<c>min_price</c>).</summary>
    [JsonPropertyName("min_price")]
    public long? MinPrice { get; set; }

    /// <summary>Suggested pay-what-you-want price, in cents (<c>suggested_price</c>).</summary>
    [JsonPropertyName("suggested_price")]
    public long? SuggestedPrice { get; set; }

    /// <summary>Whether the variant issues license keys (<c>has_license_keys</c>).</summary>
    [JsonPropertyName("has_license_keys")]
    public bool? HasLicenseKeys { get; set; }

    /// <summary>Maximum number of license activations allowed (<c>license_activation_limit</c>).</summary>
    [JsonPropertyName("license_activation_limit")]
    public int? LicenseActivationLimit { get; set; }

    /// <summary>Whether the activation limit is unlimited (<c>is_license_limit_unlimited</c>).</summary>
    [JsonPropertyName("is_license_limit_unlimited")]
    public bool? IsLicenseLimitUnlimited { get; set; }

    /// <summary>Numeric length of the license validity (<c>license_length_value</c>).</summary>
    [JsonPropertyName("license_length_value")]
    public int? LicenseLengthValue { get; set; }

    /// <summary>Unit for the license length, e.g. <c>days</c>, <c>months</c> (<c>license_length_unit</c>).</summary>
    [JsonPropertyName("license_length_unit")]
    public string? LicenseLengthUnit { get; set; }

    /// <summary>Whether the license length is unlimited (<c>is_license_length_unlimited</c>).</summary>
    [JsonPropertyName("is_license_length_unlimited")]
    public bool? IsLicenseLengthUnlimited { get; set; }

    /// <summary>Sort order of the variant within its product (<c>sort</c>).</summary>
    [JsonPropertyName("sort")]
    public int? Sort { get; set; }

    /// <summary>Current variant status (<c>status</c>).</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>Human-readable status label (<c>status_formatted</c>).</summary>
    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    /// <summary>Whether the variant was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    /// <summary>Timestamp the variant was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Timestamp the variant was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
