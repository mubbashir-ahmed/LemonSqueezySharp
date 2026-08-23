using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.SubscriptionInvoices;

/// <summary>
/// Subscription-invoice attribute payload.
/// See https://docs.lemonsqueezy.com/api/subscription-invoices.
/// </summary>
public sealed class SubscriptionInvoiceAttributes
{
    /// <summary>Id of the store this invoice belongs to (<c>store_id</c>).</summary>
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    /// <summary>Id of the subscription this invoice belongs to (<c>subscription_id</c>).</summary>
    [JsonPropertyName("subscription_id")]
    public long? SubscriptionId { get; set; }

    /// <summary>Id of the customer billed by this invoice (<c>customer_id</c>).</summary>
    [JsonPropertyName("customer_id")]
    public long? CustomerId { get; set; }

    /// <summary>Name of the billed customer (<c>user_name</c>).</summary>
    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    /// <summary>Email of the billed customer (<c>user_email</c>).</summary>
    [JsonPropertyName("user_email")]
    public string? UserEmail { get; set; }

    /// <summary>One of <c>initial</c>, <c>renewal</c>, <c>updated</c>.</summary>
    [JsonPropertyName("billing_reason")]
    public string? BillingReason { get; set; }

    /// <summary>Brand of the card used for the invoice (<c>card_brand</c>).</summary>
    [JsonPropertyName("card_brand")]
    public string? CardBrand { get; set; }

    /// <summary>Last four digits of the card used for the invoice (<c>card_last_four</c>).</summary>
    [JsonPropertyName("card_last_four")]
    public string? CardLastFour { get; set; }

    /// <summary>ISO 4217 currency code of the invoice (<c>currency</c>).</summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>Exchange rate used to convert the invoice currency to USD (<c>currency_rate</c>).</summary>
    [JsonPropertyName("currency_rate")]
    public string? CurrencyRate { get; set; }

    /// <summary>Status of the invoice (<c>status</c>).</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>Human-readable formatted status of the invoice (<c>status_formatted</c>).</summary>
    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    /// <summary>Whether the invoice has been refunded (<c>refunded</c>).</summary>
    [JsonPropertyName("refunded")]
    public bool? Refunded { get; set; }

    /// <summary>Date and time the invoice was refunded (<c>refunded_at</c>).</summary>
    [JsonPropertyName("refunded_at")]
    public DateTimeOffset? RefundedAt { get; set; }

    /// <summary>Invoice subtotal in cents, before tax and discounts (<c>subtotal</c>).</summary>
    [JsonPropertyName("subtotal")]
    public long? Subtotal { get; set; }

    /// <summary>Total discount applied to the invoice in cents (<c>discount_total</c>).</summary>
    [JsonPropertyName("discount_total")]
    public long? DiscountTotal { get; set; }

    /// <summary>Tax applied to the invoice in cents (<c>tax</c>).</summary>
    [JsonPropertyName("tax")]
    public long? Tax { get; set; }

    /// <summary>Whether the invoice total is inclusive of tax (<c>tax_inclusive</c>).</summary>
    [JsonPropertyName("tax_inclusive")]
    public bool? TaxInclusive { get; set; }

    /// <summary>Invoice total in cents (<c>total</c>).</summary>
    [JsonPropertyName("total")]
    public long? Total { get; set; }

    /// <summary>Amount refunded in cents (<c>refunded_amount</c>).</summary>
    [JsonPropertyName("refunded_amount")]
    public long? RefundedAmount { get; set; }

    /// <summary>Invoice subtotal in USD cents (<c>subtotal_usd</c>).</summary>
    [JsonPropertyName("subtotal_usd")]
    public long? SubtotalUsd { get; set; }

    /// <summary>Total discount in USD cents (<c>discount_total_usd</c>).</summary>
    [JsonPropertyName("discount_total_usd")]
    public long? DiscountTotalUsd { get; set; }

    /// <summary>Tax in USD cents (<c>tax_usd</c>).</summary>
    [JsonPropertyName("tax_usd")]
    public long? TaxUsd { get; set; }

    /// <summary>Invoice total in USD cents (<c>total_usd</c>).</summary>
    [JsonPropertyName("total_usd")]
    public long? TotalUsd { get; set; }

    /// <summary>Amount refunded in USD cents (<c>refunded_amount_usd</c>).</summary>
    [JsonPropertyName("refunded_amount_usd")]
    public long? RefundedAmountUsd { get; set; }

    /// <summary>Human-readable formatted subtotal (<c>subtotal_formatted</c>).</summary>
    [JsonPropertyName("subtotal_formatted")]
    public string? SubtotalFormatted { get; set; }

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

    /// <summary>URLs exposed on the invoice (<c>urls</c>).</summary>
    [JsonPropertyName("urls")]
    public SubscriptionInvoiceUrls? Urls { get; set; }

    /// <summary>Whether the invoice was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    /// <summary>Date and time the invoice was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the invoice was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// URLs exposed on a subscription invoice.
/// </summary>
public sealed class SubscriptionInvoiceUrls
{
    /// <summary>Presigned URL to the generated invoice PDF (<c>invoice_url</c>).</summary>
    [JsonPropertyName("invoice_url")]
    public string? InvoiceUrl { get; set; }
}
