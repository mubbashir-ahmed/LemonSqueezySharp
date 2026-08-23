namespace LemonSqueezySharp.Models.Webhooks;

/// <summary>
/// Inputs for <c>POST /v1/webhooks</c>.
/// </summary>
public sealed class CreateWebhookRequest
{
    /// <summary>Store the webhook belongs to. Required.</summary>
    public string StoreId { get; set; } = string.Empty;

    /// <summary>HTTPS URL LemonSqueezy will POST events to. Required.</summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Event names the webhook subscribes to. Required, must contain at least one entry.
    /// </summary>
    public IReadOnlyList<string> Events { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Shared secret used for HMAC signature verification of inbound events.
    /// Strongly recommended.
    /// </summary>
    public string? Secret { get; set; }
}

/// <summary>
/// Wire-format attributes sent inside the JSON:API write envelope.
/// </summary>
internal sealed class WebhookCreateAttributes
{
    [System.Text.Json.Serialization.JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [System.Text.Json.Serialization.JsonPropertyName("events")]
    public IReadOnlyList<string> Events { get; set; } = Array.Empty<string>();

    [System.Text.Json.Serialization.JsonPropertyName("secret")]
    public string? Secret { get; set; }
}
