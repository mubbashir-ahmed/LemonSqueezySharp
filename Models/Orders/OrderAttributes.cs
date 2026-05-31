using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Orders;

/// <summary>
/// Order attribute payload.
/// See https://docs.lemonsqueezy.com/api/orders.
/// </summary>
public sealed class OrderAttributes
{
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    [JsonPropertyName("customer_id")]
    public long? CustomerId { get; set; }

    [JsonPropertyName("identifier")]
    public string? Identifier { get; set; }

    [JsonPropertyName("order_number")]
    public long? OrderNumber { get; set; }

    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    [JsonPropertyName("user_email")]
    public string? UserEmail { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("currency_rate")]
    public string? CurrencyRate { get; set; }

    [JsonPropertyName("tax_name")]
    public string? TaxName { get; set; }

    [JsonPropertyName("tax_rate")]
    public string? TaxRate { get; set; }

    [JsonPropertyName("tax_inclusive")]
    public bool? TaxInclusive { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    [JsonPropertyName("refunded")]
    public bool? Refunded { get; set; }

    [JsonPropertyName("refunded_at")]
    public DateTimeOffset? RefundedAt { get; set; }

    [JsonPropertyName("subtotal")]
    public long? Subtotal { get; set; }

    [JsonPropertyName("setup_fee")]
    public long? SetupFee { get; set; }

    [JsonPropertyName("discount_total")]
    public long? DiscountTotal { get; set; }

    [JsonPropertyName("tax")]
    public long? Tax { get; set; }

    [JsonPropertyName("total")]
    public long? Total { get; set; }

    [JsonPropertyName("subtotal_usd")]
    public long? SubtotalUsd { get; set; }

    [JsonPropertyName("setup_fee_usd")]
    public long? SetupFeeUsd { get; set; }

    [JsonPropertyName("discount_total_usd")]
    public long? DiscountTotalUsd { get; set; }

    [JsonPropertyName("tax_usd")]
    public long? TaxUsd { get; set; }

    [JsonPropertyName("total_usd")]
    public long? TotalUsd { get; set; }

    [JsonPropertyName("subtotal_formatted")]
    public string? SubtotalFormatted { get; set; }

    [JsonPropertyName("setup_fee_formatted")]
    public string? SetupFeeFormatted { get; set; }

    [JsonPropertyName("discount_total_formatted")]
    public string? DiscountTotalFormatted { get; set; }

    [JsonPropertyName("tax_formatted")]
    public string? TaxFormatted { get; set; }

    [JsonPropertyName("total_formatted")]
    public string? TotalFormatted { get; set; }

    [JsonPropertyName("first_order_item")]
    public OrderFirstItem? FirstOrderItem { get; set; }

    [JsonPropertyName("urls")]
    public OrderUrls? Urls { get; set; }

    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// Flattened summary of the first item in an order, embedded into the order itself
/// for convenience by LemonSqueezy.
/// </summary>
public sealed class OrderFirstItem
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    [JsonPropertyName("product_id")]
    public long? ProductId { get; set; }

    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    [JsonPropertyName("product_name")]
    public string? ProductName { get; set; }

    [JsonPropertyName("variant_name")]
    public string? VariantName { get; set; }

    [JsonPropertyName("price")]
    public long? Price { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }

    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }
}

/// <summary>
/// Self-service URLs exposed on an order (e.g. customer-facing receipt).
/// </summary>
public sealed class OrderUrls
{
    [JsonPropertyName("receipt")]
    public string? Receipt { get; set; }
}
