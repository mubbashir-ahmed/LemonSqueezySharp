using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.LicenseKeyInstances;

/// <summary>
/// License-key-instance attribute payload — represents a single activation of a license
/// key (e.g. one device or installation).
/// See https://docs.lemonsqueezy.com/api/license-key-instances.
/// </summary>
public sealed class LicenseKeyInstanceAttributes
{
    /// <summary>Identifier of the license key this activation belongs to (<c>license_key_id</c>).</summary>
    [JsonPropertyName("license_key_id")]
    public long? LicenseKeyId { get; set; }

    /// <summary>Unique UUID identifier for this activation.</summary>
    [JsonPropertyName("identifier")]
    public string? Identifier { get; set; }

    /// <summary>Caller-supplied label for the activation (e.g. machine name).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>Date and time the activation was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the activation was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
