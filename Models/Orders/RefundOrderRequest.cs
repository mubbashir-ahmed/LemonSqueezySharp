namespace LemonSqueezySharp.Models.Orders;

/// <summary>
/// Inputs for <c>POST /v1/orders/{id}/refund</c>.
/// </summary>
public sealed class RefundOrderRequest
{
    /// <summary>
    /// Amount to refund, in the order's currency, expressed in cents.
    /// Omit to issue a full refund of the order total.
    /// </summary>
    public long? Amount { get; set; }
}

/// <summary>
/// Wire shape sent to LemonSqueezy inside the JSON:API write envelope's
/// <c>attributes</c> block for a refund.
/// </summary>
internal sealed class RefundOrderAttributes
{
    [System.Text.Json.Serialization.JsonPropertyName("amount")]
    public long? Amount { get; set; }
}
