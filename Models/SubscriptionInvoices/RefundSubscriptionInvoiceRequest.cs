namespace LemonSqueezySharp.Models.SubscriptionInvoices;

/// <summary>
/// Inputs for <c>POST /v1/subscription-invoices/{id}/refund</c>.
/// </summary>
public sealed class RefundSubscriptionInvoiceRequest
{
    /// <summary>
    /// Amount to refund, in the invoice's currency, expressed in cents.
    /// Omit to issue a full refund of the invoice total.
    /// </summary>
    public long? Amount { get; set; }
}

/// <summary>
/// Wire shape sent to LemonSqueezy inside the JSON:API write envelope's
/// <c>attributes</c> block for a subscription-invoice refund.
/// </summary>
internal sealed class RefundSubscriptionInvoiceAttributes
{
    [System.Text.Json.Serialization.JsonPropertyName("amount")]
    public long? Amount { get; set; }
}
