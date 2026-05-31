using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.DiscountRedemptions;

/// <summary>
/// Discount-redemption attribute payload — a snapshot of the discount as applied to a
/// specific order.
/// See https://docs.lemonsqueezy.com/api/discount-redemptions.
/// </summary>
public sealed class DiscountRedemptionAttributes
{
    [JsonPropertyName("discount_id")]
    public long? DiscountId { get; set; }

    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    [JsonPropertyName("discount_name")]
    public string? DiscountName { get; set; }

    [JsonPropertyName("discount_code")]
    public string? DiscountCode { get; set; }

    [JsonPropertyName("discount_amount")]
    public long? DiscountAmount { get; set; }

    /// <summary>Either <c>"percent"</c> or <c>"fixed"</c>.</summary>
    [JsonPropertyName("discount_amount_type")]
    public string? DiscountAmountType { get; set; }

    /// <summary>
    /// The actual amount redeemed on the order, in cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
