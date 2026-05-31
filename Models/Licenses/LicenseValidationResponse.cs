using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Licenses;

/// <summary>
/// Response of <c>POST /v1/licenses/validate</c>.
/// </summary>
public sealed class LicenseValidationResponse
{
    [JsonPropertyName("valid")]
    public bool Valid { get; set; }

    /// <summary>Populated when <see cref="Valid"/> is <c>false</c>.</summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("license_key")]
    public LicenseInfo? LicenseKey { get; set; }

    /// <summary>Present when an <c>instance_id</c> was supplied with the request.</summary>
    [JsonPropertyName("instance")]
    public LicenseInstanceInfo? Instance { get; set; }

    [JsonPropertyName("meta")]
    public LicenseMeta? Meta { get; set; }
}
