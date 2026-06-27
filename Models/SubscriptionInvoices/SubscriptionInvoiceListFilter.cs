using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.SubscriptionInvoices;

/// <summary>
/// Filter for <c>GET /v1/subscription-invoices</c>.
/// </summary>
public sealed class SubscriptionInvoiceListFilter : ListFilter
{
    /// <summary>
    /// Filter invoices by store id (<c>filter[store_id]</c>).
    /// </summary>
    public string? StoreId { get; set; }

    /// <summary>
    /// Filter invoices by subscription id (<c>filter[subscription_id]</c>).
    /// </summary>
    public string? SubscriptionId { get; set; }

    /// <summary>
    /// Filter invoices by customer email address (<c>filter[user_email]</c>).
    /// </summary>
    public string? UserEmail { get; set; }

    /// <summary>One of <c>paid</c>, <c>void</c>, <c>refunded</c>.</summary>
    public string? Status { get; set; }

    /// <summary>
    /// Filter invoices by whether they have been refunded (<c>filter[refunded]</c>).
    /// </summary>
    public bool? Refunded { get; set; }
}
