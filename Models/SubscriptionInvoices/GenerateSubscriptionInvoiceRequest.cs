namespace LemonSqueezy.NET.Models.SubscriptionInvoices;

/// <summary>
/// Optional overrides for <c>POST /v1/subscription-invoices/{id}/generate-invoice</c>.
/// All fields are passed as query-string parameters by LemonSqueezy.
/// </summary>
public sealed class GenerateSubscriptionInvoiceRequest
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public string? Notes { get; set; }

    /// <summary>
    /// ISO 639 language code used to localise the generated invoice (e.g. <c>"en"</c>).
    /// </summary>
    public string? Locale { get; set; }
}
