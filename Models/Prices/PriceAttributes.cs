using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.Prices;

/// <summary>
/// Price attribute payload.
/// See https://docs.lemonsqueezy.com/api/prices.
/// </summary>
public sealed class PriceAttributes
{
    /// <summary>Identifier of the variant the price belongs to (<c>variant_id</c>).</summary>
    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    /// <summary>One of <c>one_time</c>, <c>subscription</c>, <c>lead_magnet</c>, <c>pwyw</c>.</summary>
    [JsonPropertyName("category")]
    public string? Category { get; set; }

    /// <summary>One of <c>standard</c>, <c>package</c>, <c>graduated</c>, <c>volume</c>.</summary>
    [JsonPropertyName("scheme")]
    public string? Scheme { get; set; }

    /// <summary>Usage aggregation method for metered billing (<c>usage_aggregation</c>).</summary>
    [JsonPropertyName("usage_aggregation")]
    public string? UsageAggregation { get; set; }

    /// <summary>Unit price, in cents (<c>unit_price</c>).</summary>
    [JsonPropertyName("unit_price")]
    public long? UnitPrice { get; set; }

    /// <summary>Unit price as a decimal string, for sub-cent precision (<c>unit_price_decimal</c>).</summary>
    [JsonPropertyName("unit_price_decimal")]
    public string? UnitPriceDecimal { get; set; }

    /// <summary>Whether a setup fee is charged (<c>setup_fee_enabled</c>).</summary>
    [JsonPropertyName("setup_fee_enabled")]
    public bool? SetupFeeEnabled { get; set; }

    /// <summary>Setup fee amount, in cents (<c>setup_fee</c>).</summary>
    [JsonPropertyName("setup_fee")]
    public long? SetupFee { get; set; }

    /// <summary>Number of units sold per package (<c>package_size</c>).</summary>
    [JsonPropertyName("package_size")]
    public int? PackageSize { get; set; }

    /// <summary>Pricing tiers for graduated / volume schemes (<c>tiers</c>).</summary>
    [JsonPropertyName("tiers")]
    public IReadOnlyList<PriceTier>? Tiers { get; set; }

    /// <summary>Subscription renewal interval unit (<c>renewal_interval_unit</c>).</summary>
    [JsonPropertyName("renewal_interval_unit")]
    public string? RenewalIntervalUnit { get; set; }

    /// <summary>Number of renewal intervals between billings (<c>renewal_interval_quantity</c>).</summary>
    [JsonPropertyName("renewal_interval_quantity")]
    public int? RenewalIntervalQuantity { get; set; }

    /// <summary>Free-trial interval unit (<c>trial_interval_unit</c>).</summary>
    [JsonPropertyName("trial_interval_unit")]
    public string? TrialIntervalUnit { get; set; }

    /// <summary>Number of trial intervals (<c>trial_interval_quantity</c>).</summary>
    [JsonPropertyName("trial_interval_quantity")]
    public int? TrialIntervalQuantity { get; set; }

    /// <summary>Minimum pay-what-you-want price, in cents (<c>min_price</c>).</summary>
    [JsonPropertyName("min_price")]
    public long? MinPrice { get; set; }

    /// <summary>Suggested pay-what-you-want price, in cents (<c>suggested_price</c>).</summary>
    [JsonPropertyName("suggested_price")]
    public long? SuggestedPrice { get; set; }

    /// <summary>Tax code applied to the price (<c>tax_code</c>).</summary>
    [JsonPropertyName("tax_code")]
    public string? TaxCode { get; set; }

    /// <summary>Timestamp the price was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Timestamp the price was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// Single tier in a graduated / volume pricing scheme.
/// </summary>
public sealed class PriceTier
{
    /// <summary>Upper unit bound for this tier, or <c>inf</c> for the final tier (<c>last_unit</c>).</summary>
    [JsonPropertyName("last_unit")]
    public string? LastUnit { get; set; }

    /// <summary>Unit price for this tier, in cents (<c>unit_price</c>).</summary>
    [JsonPropertyName("unit_price")]
    public long? UnitPrice { get; set; }

    /// <summary>Unit price for this tier as a decimal string (<c>unit_price_decimal</c>).</summary>
    [JsonPropertyName("unit_price_decimal")]
    public string? UnitPriceDecimal { get; set; }

    /// <summary>Fixed fee applied for this tier, in cents (<c>fixed_fee</c>).</summary>
    [JsonPropertyName("fixed_fee")]
    public long? FixedFee { get; set; }
}
