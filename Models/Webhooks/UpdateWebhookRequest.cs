namespace LemonSqueezySharp.Models.Webhooks;

/// <summary>
/// Inputs for <c>PATCH /v1/webhooks/{id}</c>. Only set the fields you want to update;
/// <see langword="null"/> values are dropped from the wire payload.
/// </summary>
public sealed class UpdateWebhookRequest
{
    /// <summary>New HTTPS URL LemonSqueezy will POST events to.</summary>
    public string? Url { get; set; }

    /// <summary>Replacement set of event names the webhook subscribes to.</summary>
    public IReadOnlyList<string>? Events { get; set; }

    /// <summary>New shared secret used for HMAC signature verification of inbound events.</summary>
    public string? Secret { get; set; }
}

/// <summary>
/// Wire-format attributes sent inside the JSON:API write envelope.
/// </summary>
internal sealed class WebhookUpdateAttributes
{
    [System.Text.Json.Serialization.JsonPropertyName("url")]
    public string? Url { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("events")]
    public IReadOnlyList<string>? Events { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("secret")]
    public string? Secret { get; set; }
}
