using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Licenses;

/// <summary>
/// Response of <c>POST /v1/licenses/deactivate</c>.
/// </summary>
public sealed class LicenseDeactivationResponse
{
    [JsonPropertyName("deactivated")]
    public bool Deactivated { get; set; }

    /// <summary>Populated when <see cref="Deactivated"/> is <c>false</c>.</summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("license_key")]
    public LicenseInfo? LicenseKey { get; set; }

    [JsonPropertyName("meta")]
    public LicenseMeta? Meta { get; set; }
}
