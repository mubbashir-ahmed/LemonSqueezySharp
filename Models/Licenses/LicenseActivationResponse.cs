using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.Licenses;

/// <summary>
/// Response of <c>POST /v1/licenses/activate</c>.
/// </summary>
public sealed class LicenseActivationResponse
{
    /// <summary>Whether the license key was successfully activated (<c>activated</c>).</summary>
    [JsonPropertyName("activated")]
    public bool Activated { get; set; }

    /// <summary>Populated when <see cref="Activated"/> is <c>false</c>.</summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>Summary of the activated license key (<c>license_key</c>).</summary>
    [JsonPropertyName("license_key")]
    public LicenseInfo? LicenseKey { get; set; }

    /// <summary>Details of the activation instance that was created (<c>instance</c>).</summary>
    [JsonPropertyName("instance")]
    public LicenseInstanceInfo? Instance { get; set; }

    /// <summary>Order and customer context for the license key (<c>meta</c>).</summary>
    [JsonPropertyName("meta")]
    public LicenseMeta? Meta { get; set; }
}
