namespace LemonSqueezy.NET.Models.Orders;

/// <summary>
/// Optional overrides for <c>POST /v1/orders/{id}/generate-invoice</c>.
/// All fields are passed as query-string parameters by LemonSqueezy.
/// </summary>
public sealed class GenerateOrderInvoiceRequest
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public string? Notes { get; set; }

    /// <summary>
    /// ISO 639 language code used to localise the generated invoice (e.g. <c>"en"</c>, <c>"fr"</c>, <c>"de"</c>).
    /// </summary>
    public string? Locale { get; set; }
}
