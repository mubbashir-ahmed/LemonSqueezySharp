namespace LemonSqueezy.NET.Models.LicenseKeys;

/// <summary>
/// Inputs for <c>PATCH /v1/license-keys/{id}</c>. Only set the fields you want to update —
/// <see langword="null"/> values are dropped from the wire payload.
/// </summary>
public sealed class UpdateLicenseKeyRequest
{
    /// <summary>
    /// Maximum number of activations allowed. Set to <c>0</c> to allow unlimited activations.
    /// </summary>
    public int? ActivationLimit { get; set; }

    /// <summary>Disable (<c>true</c>) or re-enable (<c>false</c>) the license key.</summary>
    public bool? Disabled { get; set; }

    /// <summary>Override the license-key expiration date.</summary>
    public DateTimeOffset? ExpiresAt { get; set; }
}

/// <summary>
/// Wire-format attributes sent inside the JSON:API write envelope.
/// </summary>
internal sealed class LicenseKeyUpdateAttributes
{
    [System.Text.Json.Serialization.JsonPropertyName("activation_limit")]
    public int? ActivationLimit { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("disabled")]
    public bool? Disabled { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }
}
