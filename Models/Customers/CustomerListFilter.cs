using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.Customers;

/// <summary>
/// Filter for <c>GET /v1/customers</c>. Extends the standard <see cref="ListFilter"/>
/// (paging + <c>include</c>) with customer-specific server-side filters.
/// </summary>
public sealed class CustomerListFilter : ListFilter
{
    /// <summary>
    /// Filter customers to the supplied store id (<c>filter[store_id]</c>).
    /// </summary>
    public string? StoreId { get; set; }

    /// <summary>
    /// Filter customers by email (<c>filter[email]</c>).
    /// </summary>
    public string? Email { get; set; }
}
