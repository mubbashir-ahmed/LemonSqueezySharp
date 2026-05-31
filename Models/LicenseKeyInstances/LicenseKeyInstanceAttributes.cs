using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.LicenseKeyInstances;

/// <summary>
/// License-key-instance attribute payload — represents a single activation of a license
/// key (e.g. one device or installation).
/// See https://docs.lemonsqueezy.com/api/license-key-instances.
/// </summary>
public sealed class LicenseKeyInstanceAttributes
{
    [JsonPropertyName("license_key_id")]
    public long? LicenseKeyId { get; set; }

    /// <summary>Unique UUID identifier for this activation.</summary>
    [JsonPropertyName("identifier")]
    public string? Identifier { get; set; }

    /// <summary>Caller-supplied label for the activation (e.g. machine name).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
