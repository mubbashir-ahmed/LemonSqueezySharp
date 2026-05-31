using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.LicenseKeys;

/// <summary>
/// Filter for <c>GET /v1/license-keys</c>.
/// </summary>
public sealed class LicenseKeyListFilter : ListFilter
{
    public string? StoreId { get; set; }
    public string? OrderId { get; set; }
    public string? OrderItemId { get; set; }
    public string? ProductId { get; set; }

    /// <summary>One of <c>inactive</c>, <c>active</c>, <c>expired</c>, <c>disabled</c>.</summary>
    public string? Status { get; set; }
}
