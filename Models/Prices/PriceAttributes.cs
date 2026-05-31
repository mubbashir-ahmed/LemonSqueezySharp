using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Prices;

/// <summary>
/// Price attribute payload.
/// See https://docs.lemonsqueezy.com/api/prices.
/// </summary>
public sealed class PriceAttributes
{
    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    /// <summary>One of <c>one_time</c>, <c>subscription</c>, <c>lead_magnet</c>, <c>pwyw</c>.</summary>
    [JsonPropertyName("category")]
    public string? Category { get; set; }

    /// <summary>One of <c>standard</c>, <c>package</c>, <c>graduated</c>, <c>volume</c>.</summary>
    [JsonPropertyName("scheme")]
    public string? Scheme { get; set; }

    [JsonPropertyName("usage_aggregation")]
    public string? UsageAggregation { get; set; }

    [JsonPropertyName("unit_price")]
    public long? UnitPrice { get; set; }

    [JsonPropertyName("unit_price_decimal")]
    public string? UnitPriceDecimal { get; set; }

    [JsonPropertyName("setup_fee_enabled")]
    public bool? SetupFeeEnabled { get; set; }

    [JsonPropertyName("setup_fee")]
    public long? SetupFee { get; set; }

    [JsonPropertyName("package_size")]
    public int? PackageSize { get; set; }

    [JsonPropertyName("tiers")]
    public IReadOnlyList<PriceTier>? Tiers { get; set; }

    [JsonPropertyName("renewal_interval_unit")]
    public string? RenewalIntervalUnit { get; set; }

    [JsonPropertyName("renewal_interval_quantity")]
    public int? RenewalIntervalQuantity { get; set; }

    [JsonPropertyName("trial_interval_unit")]
    public string? TrialIntervalUnit { get; set; }

    [JsonPropertyName("trial_interval_quantity")]
    public int? TrialIntervalQuantity { get; set; }

    [JsonPropertyName("min_price")]
    public long? MinPrice { get; set; }

    [JsonPropertyName("suggested_price")]
    public long? SuggestedPrice { get; set; }

    [JsonPropertyName("tax_code")]
    public string? TaxCode { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// Single tier in a graduated / volume pricing scheme.
/// </summary>
public sealed class PriceTier
{
    [JsonPropertyName("last_unit")]
    public string? LastUnit { get; set; }

    [JsonPropertyName("unit_price")]
    public long? UnitPrice { get; set; }

    [JsonPropertyName("unit_price_decimal")]
    public string? UnitPriceDecimal { get; set; }

    [JsonPropertyName("fixed_fee")]
    public long? FixedFee { get; set; }
}
