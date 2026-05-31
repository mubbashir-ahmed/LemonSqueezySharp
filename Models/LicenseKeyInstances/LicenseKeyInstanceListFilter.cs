using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.LicenseKeyInstances;

/// <summary>
/// Filter for <c>GET /v1/license-key-instances</c>.
/// </summary>
public sealed class LicenseKeyInstanceListFilter : ListFilter
{
    public string? LicenseKeyId { get; set; }
}
