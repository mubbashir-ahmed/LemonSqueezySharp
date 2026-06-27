using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.LicenseKeys;

/// <summary>
/// Filter for <c>GET /v1/license-keys</c>.
/// </summary>
public sealed class LicenseKeyListFilter : ListFilter
{
    /// <summary>Only return license keys belonging to this store.</summary>
    public string? StoreId { get; set; }

    /// <summary>Only return license keys created by this order.</summary>
    public string? OrderId { get; set; }

    /// <summary>Only return license keys created by this order item.</summary>
    public string? OrderItemId { get; set; }

    /// <summary>Only return license keys associated with this product.</summary>
    public string? ProductId { get; set; }

    /// <summary>One of <c>inactive</c>, <c>active</c>, <c>expired</c>, <c>disabled</c>.</summary>
    public string? Status { get; set; }
}
