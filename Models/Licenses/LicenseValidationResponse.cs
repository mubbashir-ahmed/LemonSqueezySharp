using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.Licenses;

/// <summary>
/// Response of <c>POST /v1/licenses/validate</c>.
/// </summary>
public sealed class LicenseValidationResponse
{
    /// <summary>Whether the license key is valid (<c>valid</c>).</summary>
    [JsonPropertyName("valid")]
    public bool Valid { get; set; }

    /// <summary>Populated when <see cref="Valid"/> is <c>false</c>.</summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>Summary of the validated license key (<c>license_key</c>).</summary>
    [JsonPropertyName("license_key")]
    public LicenseInfo? LicenseKey { get; set; }

    /// <summary>Present when an <c>instance_id</c> was supplied with the request.</summary>
    [JsonPropertyName("instance")]
    public LicenseInstanceInfo? Instance { get; set; }

    /// <summary>Order and customer context for the license key (<c>meta</c>).</summary>
    [JsonPropertyName("meta")]
    public LicenseMeta? Meta { get; set; }
}
