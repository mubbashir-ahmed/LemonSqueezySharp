using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.Orders;

/// <summary>
/// Order attribute payload.
/// See https://docs.lemonsqueezy.com/api/orders.
/// </summary>
public sealed class OrderAttributes
{
    /// <summary>Id of the store this order belongs to (<c>store_id</c>).</summary>
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    /// <summary>Id of the customer who placed the order (<c>customer_id</c>).</summary>
    [JsonPropertyName("customer_id")]
    public long? CustomerId { get; set; }

    /// <summary>Unique order identifier string (<c>identifier</c>).</summary>
    [JsonPropertyName("identifier")]
    public string? Identifier { get; set; }

    /// <summary>Sequential order number within the store (<c>order_number</c>).</summary>
    [JsonPropertyName("order_number")]
    public long? OrderNumber { get; set; }

    /// <summary>Name of the customer who placed the order (<c>user_name</c>).</summary>
    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    /// <summary>Email of the customer who placed the order (<c>user_email</c>).</summary>
    [JsonPropertyName("user_email")]
    public string? UserEmail { get; set; }

    /// <summary>ISO 4217 currency code of the order (<c>currency</c>).</summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>Exchange rate used to convert the order currency to USD (<c>currency_rate</c>).</summary>
    [JsonPropertyName("currency_rate")]
    public string? CurrencyRate { get; set; }

    /// <summary>Name of the tax applied to the order (<c>tax_name</c>).</summary>
    [JsonPropertyName("tax_name")]
    public string? TaxName { get; set; }

    /// <summary>Tax rate applied to the order (<c>tax_rate</c>).</summary>
    [JsonPropertyName("tax_rate")]
    public string? TaxRate { get; set; }

    /// <summary>Whether the order total is inclusive of tax (<c>tax_inclusive</c>).</summary>
    [JsonPropertyName("tax_inclusive")]
    public bool? TaxInclusive { get; set; }

    /// <summary>Status of the order (<c>status</c>).</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>Human-readable formatted status of the order (<c>status_formatted</c>).</summary>
    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    /// <summary>Whether the order has been refunded (<c>refunded</c>).</summary>
    [JsonPropertyName("refunded")]
    public bool? Refunded { get; set; }

    /// <summary>Date and time the order was refunded (<c>refunded_at</c>).</summary>
    [JsonPropertyName("refunded_at")]
    public DateTimeOffset? RefundedAt { get; set; }

    /// <summary>Order subtotal in cents, before tax and discounts (<c>subtotal</c>).</summary>
    [JsonPropertyName("subtotal")]
    public long? Subtotal { get; set; }

    /// <summary>Setup fee in cents (<c>setup_fee</c>).</summary>
    [JsonPropertyName("setup_fee")]
    public long? SetupFee { get; set; }

    /// <summary>Total discount applied to the order in cents (<c>discount_total</c>).</summary>
    [JsonPropertyName("discount_total")]
    public long? DiscountTotal { get; set; }

    /// <summary>Tax applied to the order in cents (<c>tax</c>).</summary>
    [JsonPropertyName("tax")]
    public long? Tax { get; set; }

    /// <summary>Order total in cents (<c>total</c>).</summary>
    [JsonPropertyName("total")]
    public long? Total { get; set; }

    /// <summary>Amount refunded in cents (<c>refunded_amount</c>).</summary>
    [JsonPropertyName("refunded_amount")]
    public long? RefundedAmount { get; set; }

    /// <summary>Order subtotal in USD cents (<c>subtotal_usd</c>).</summary>
    [JsonPropertyName("subtotal_usd")]
    public long? SubtotalUsd { get; set; }

    /// <summary>Setup fee in USD cents (<c>setup_fee_usd</c>).</summary>
    [JsonPropertyName("setup_fee_usd")]
    public long? SetupFeeUsd { get; set; }

    /// <summary>Total discount in USD cents (<c>discount_total_usd</c>).</summary>
    [JsonPropertyName("discount_total_usd")]
    public long? DiscountTotalUsd { get; set; }

    /// <summary>Tax in USD cents (<c>tax_usd</c>).</summary>
    [JsonPropertyName("tax_usd")]
    public long? TaxUsd { get; set; }

    /// <summary>Order total in USD cents (<c>total_usd</c>).</summary>
    [JsonPropertyName("total_usd")]
    public long? TotalUsd { get; set; }

    /// <summary>Amount refunded in USD cents (<c>refunded_amount_usd</c>).</summary>
    [JsonPropertyName("refunded_amount_usd")]
    public long? RefundedAmountUsd { get; set; }

    /// <summary>Human-readable formatted subtotal (<c>subtotal_formatted</c>).</summary>
    [JsonPropertyName("subtotal_formatted")]
    public string? SubtotalFormatted { get; set; }

    /// <summary>Human-readable formatted setup fee (<c>setup_fee_formatted</c>).</summary>
    [JsonPropertyName("setup_fee_formatted")]
    public string? SetupFeeFormatted { get; set; }

    /// <summary>Human-readable formatted discount total (<c>discount_total_formatted</c>).</summary>
    [JsonPropertyName("discount_total_formatted")]
    public string? DiscountTotalFormatted { get; set; }

    /// <summary>Human-readable formatted tax (<c>tax_formatted</c>).</summary>
    [JsonPropertyName("tax_formatted")]
    public string? TaxFormatted { get; set; }

    /// <summary>Human-readable formatted total (<c>total_formatted</c>).</summary>
    [JsonPropertyName("total_formatted")]
    public string? TotalFormatted { get; set; }

    /// <summary>Human-readable formatted refunded amount (<c>refunded_amount_formatted</c>).</summary>
    [JsonPropertyName("refunded_amount_formatted")]
    public string? RefundedAmountFormatted { get; set; }

    /// <summary>Summary of the first item in the order (<c>first_order_item</c>).</summary>
    [JsonPropertyName("first_order_item")]
    public OrderFirstItem? FirstOrderItem { get; set; }

    /// <summary>Self-service URLs exposed on the order (<c>urls</c>).</summary>
    [JsonPropertyName("urls")]
    public OrderUrls? Urls { get; set; }

    /// <summary>Whether the order was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    /// <summary>Date and time the order was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the order was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// Flattened summary of the first item in an order, embedded into the order itself
/// for convenience by LemonSqueezy.
/// </summary>
public sealed class OrderFirstItem
{
    /// <summary>Id of the order item (<c>id</c>).</summary>
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    /// <summary>Id of the order this item belongs to (<c>order_id</c>).</summary>
    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    /// <summary>Id of the purchased product (<c>product_id</c>).</summary>
    [JsonPropertyName("product_id")]
    public long? ProductId { get; set; }

    /// <summary>Id of the purchased variant (<c>variant_id</c>).</summary>
    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    /// <summary>Name of the purchased product (<c>product_name</c>).</summary>
    [JsonPropertyName("product_name")]
    public string? ProductName { get; set; }

    /// <summary>Name of the purchased variant (<c>variant_name</c>).</summary>
    [JsonPropertyName("variant_name")]
    public string? VariantName { get; set; }

    /// <summary>Price of the item in cents (<c>price</c>).</summary>
    [JsonPropertyName("price")]
    public long? Price { get; set; }

    /// <summary>Date and time the item was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the item was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>Whether the item was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }
}

/// <summary>
/// Self-service URLs exposed on an order (e.g. customer-facing receipt).
/// </summary>
public sealed class OrderUrls
{
    /// <summary>Customer-facing receipt URL for the order (<c>receipt</c>).</summary>
    [JsonPropertyName("receipt")]
    public string? Receipt { get; set; }
}
