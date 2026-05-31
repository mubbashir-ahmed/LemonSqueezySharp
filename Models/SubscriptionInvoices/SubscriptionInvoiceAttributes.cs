using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.SubscriptionInvoices;

/// <summary>
/// Subscription-invoice attribute payload.
/// See https://docs.lemonsqueezy.com/api/subscription-invoices.
/// </summary>
public sealed class SubscriptionInvoiceAttributes
{
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    [JsonPropertyName("subscription_id")]
    public long? SubscriptionId { get; set; }

    [JsonPropertyName("customer_id")]
    public long? CustomerId { get; set; }

    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    [JsonPropertyName("user_email")]
    public string? UserEmail { get; set; }

    /// <summary>One of <c>initial</c>, <c>renewal</c>, <c>updated</c>.</summary>
    [JsonPropertyName("billing_reason")]
    public string? BillingReason { get; set; }

    [JsonPropertyName("card_brand")]
    public string? CardBrand { get; set; }

    [JsonPropertyName("card_last_four")]
    public string? CardLastFour { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("currency_rate")]
    public string? CurrencyRate { get; set; }

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

    [JsonPropertyName("discount_total")]
    public long? DiscountTotal { get; set; }

    [JsonPropertyName("tax")]
    public long? Tax { get; set; }

    [JsonPropertyName("tax_inclusive")]
    public bool? TaxInclusive { get; set; }

    [JsonPropertyName("total")]
    public long? Total { get; set; }

    [JsonPropertyName("refunded_amount")]
    public long? RefundedAmount { get; set; }

    [JsonPropertyName("subtotal_usd")]
    public long? SubtotalUsd { get; set; }

    [JsonPropertyName("discount_total_usd")]
    public long? DiscountTotalUsd { get; set; }

    [JsonPropertyName("tax_usd")]
    public long? TaxUsd { get; set; }

    [JsonPropertyName("total_usd")]
    public long? TotalUsd { get; set; }

    [JsonPropertyName("refunded_amount_usd")]
    public long? RefundedAmountUsd { get; set; }

    [JsonPropertyName("subtotal_formatted")]
    public string? SubtotalFormatted { get; set; }

    [JsonPropertyName("discount_total_formatted")]
    public string? DiscountTotalFormatted { get; set; }

    [JsonPropertyName("tax_formatted")]
    public string? TaxFormatted { get; set; }

    [JsonPropertyName("total_formatted")]
    public string? TotalFormatted { get; set; }

    [JsonPropertyName("refunded_amount_formatted")]
    public string? RefundedAmountFormatted { get; set; }

    [JsonPropertyName("urls")]
    public SubscriptionInvoiceUrls? Urls { get; set; }

    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// URLs exposed on a subscription invoice.
/// </summary>
public sealed class SubscriptionInvoiceUrls
{
    [JsonPropertyName("invoice_url")]
    public string? InvoiceUrl { get; set; }
}
