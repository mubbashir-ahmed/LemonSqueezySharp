using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.DiscountRedemptions;

/// <summary>
/// Discount-redemption attribute payload — a snapshot of the discount as applied to a
/// specific order.
/// See https://docs.lemonsqueezy.com/api/discount-redemptions.
/// </summary>
public sealed class DiscountRedemptionAttributes
{
    /// <summary>Identifier of the discount that was redeemed (<c>discount_id</c>).</summary>
    [JsonPropertyName("discount_id")]
    public long? DiscountId { get; set; }

    /// <summary>Identifier of the order the discount was applied to (<c>order_id</c>).</summary>
    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    /// <summary>Name of the discount at the time of redemption (<c>discount_name</c>).</summary>
    [JsonPropertyName("discount_name")]
    public string? DiscountName { get; set; }

    /// <summary>Discount code at the time of redemption (<c>discount_code</c>).</summary>
    [JsonPropertyName("discount_code")]
    public string? DiscountCode { get; set; }

    /// <summary>Configured discount amount; a percentage or a cent value depending on <see cref="DiscountAmountType"/> (<c>discount_amount</c>).</summary>
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

    /// <summary>Date and time the redemption was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the redemption was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
