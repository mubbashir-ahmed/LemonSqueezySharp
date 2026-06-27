using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Licenses;

/// <summary>
/// Response of <c>POST /v1/licenses/deactivate</c>.
/// </summary>
public sealed class LicenseDeactivationResponse
{
    /// <summary>Whether the license key instance was successfully deactivated (<c>deactivated</c>).</summary>
    [JsonPropertyName("deactivated")]
    public bool Deactivated { get; set; }

    /// <summary>Populated when <see cref="Deactivated"/> is <c>false</c>.</summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>Summary of the license key (<c>license_key</c>).</summary>
    [JsonPropertyName("license_key")]
    public LicenseInfo? LicenseKey { get; set; }

    /// <summary>Order and customer context for the license key (<c>meta</c>).</summary>
    [JsonPropertyName("meta")]
    public LicenseMeta? Meta { get; set; }
}
