namespace LemonSqueezySharp.Models.Orders;

/// <summary>
/// Optional overrides for <c>POST /v1/orders/{id}/generate-invoice</c>.
/// All fields are passed as query-string parameters by LemonSqueezy.
/// </summary>
public sealed class GenerateOrderInvoiceRequest
{
    /// <summary>Override the billing name shown on the invoice.</summary>
    public string? Name { get; set; }

    /// <summary>Override the billing street address shown on the invoice.</summary>
    public string? Address { get; set; }

    /// <summary>Override the billing city shown on the invoice.</summary>
    public string? City { get; set; }

    /// <summary>Override the billing state / region shown on the invoice.</summary>
    public string? State { get; set; }

    /// <summary>Override the billing country shown on the invoice.</summary>
    public string? Country { get; set; }

    /// <summary>Override the billing ZIP / postal code shown on the invoice.</summary>
    public string? ZipCode { get; set; }

    /// <summary>Additional notes to include on the invoice.</summary>
    public string? Notes { get; set; }

    /// <summary>
    /// ISO 639 language code used to localise the generated invoice (e.g. <c>"en"</c>, <c>"fr"</c>, <c>"de"</c>).
    /// </summary>
    public string? Locale { get; set; }
}
