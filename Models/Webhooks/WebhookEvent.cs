using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.Webhooks;

/// <summary>
/// Inbound webhook payload from LemonSqueezy. The <c>data</c> shape varies by event type;
/// it is preserved as a <see cref="JsonElement"/> so callers can deserialize against the
/// appropriate resource model.
/// </summary>
public sealed class WebhookEvent
{
    /// <summary>
    /// The <c>meta</c> block describing the event (event name, custom data, originating webhook id).
    /// </summary>
    [JsonPropertyName("meta")]
    public WebhookMeta? Meta { get; set; }

    /// <summary>
    /// The raw <c>data</c> element exactly as received; deserialize it against the resource model
    /// appropriate for the event type.
    /// </summary>
    [JsonPropertyName("data")]
    public JsonElement Data { get; set; }
}

/// <summary>
/// The <c>meta</c> block of an inbound webhook payload, carrying the event name, any custom data
/// attached at checkout, and the originating webhook id.
/// </summary>
public sealed class WebhookMeta
{
    /// <summary>The LemonSqueezy <c>event_name</c> for this payload, e.g. <c>order_created</c>.</summary>
    [JsonPropertyName("event_name")]
    public string? EventName { get; set; }

    /// <summary>Arbitrary key/value data attached to the originating checkout, if any.</summary>
    [JsonPropertyName("custom_data")]
    public JsonElement? CustomData { get; set; }

    /// <summary>Identifier of the webhook subscription that delivered this event.</summary>
    [JsonPropertyName("webhook_id")]
    public string? WebhookId { get; set; }
}
