using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.LicenseKeyInstances;

/// <summary>
/// Filter for <c>GET /v1/license-key-instances</c>.
/// </summary>
public sealed class LicenseKeyInstanceListFilter : ListFilter
{
    /// <summary>Only return instances belonging to this license key.</summary>
    public string? LicenseKeyId { get; set; }
}
