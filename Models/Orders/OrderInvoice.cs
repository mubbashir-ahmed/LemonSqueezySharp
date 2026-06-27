using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Orders;

/// <summary>
/// Response payload from <c>POST /v1/orders/{id}/generate-invoice</c> — exposes a
/// short-lived presigned URL to the generated invoice PDF.
/// </summary>
public sealed class OrderInvoice
{
    /// <summary>
    /// Presigned URL to the generated invoice PDF (<c>invoice_url</c>).
    /// </summary>
    [JsonPropertyName("invoice_url")]
    public string? InvoiceUrl { get; set; }
}
