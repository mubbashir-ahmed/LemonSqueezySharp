using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Webhooks;

/// <summary>
/// Inbound webhook payload from LemonSqueezy. The <c>data</c> shape varies by event type;
/// it is preserved as a <see cref="JsonElement"/> so callers can deserialize against the
/// appropriate resource model.
/// </summary>
public sealed class WebhookEvent
{
    [JsonPropertyName("meta")]
    public WebhookMeta? Meta { get; set; }

    [JsonPropertyName("data")]
    public JsonElement Data { get; set; }
}

public sealed class WebhookMeta
{
    [JsonPropertyName("event_name")]
    public string? EventName { get; set; }

    [JsonPropertyName("custom_data")]
    public JsonElement? CustomData { get; set; }

    [JsonPropertyName("webhook_id")]
    public string? WebhookId { get; set; }
}
