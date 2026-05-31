using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Licenses;

/// <summary>
/// Response of <c>POST /v1/licenses/activate</c>.
/// </summary>
public sealed class LicenseActivationResponse
{
    [JsonPropertyName("activated")]
    public bool Activated { get; set; }

    /// <summary>Populated when <see cref="Activated"/> is <c>false</c>.</summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("license_key")]
    public LicenseInfo? LicenseKey { get; set; }

    [JsonPropertyName("instance")]
    public LicenseInstanceInfo? Instance { get; set; }

    [JsonPropertyName("meta")]
    public LicenseMeta? Meta { get; set; }
}
