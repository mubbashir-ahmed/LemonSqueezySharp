using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.SubscriptionInvoices;

/// <summary>
/// Filter for <c>GET /v1/subscription-invoices</c>.
/// </summary>
public sealed class SubscriptionInvoiceListFilter : ListFilter
{
    public string? StoreId { get; set; }
    public string? SubscriptionId { get; set; }
    public string? UserEmail { get; set; }

    /// <summary>One of <c>paid</c>, <c>void</c>, <c>refunded</c>.</summary>
    public string? Status { get; set; }

    public bool? Refunded { get; set; }
}
