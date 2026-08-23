using System.Text.Json;
using LemonSqueezySharp.Serialization;

namespace LemonSqueezySharp.Models.Webhooks.Events;

/// <summary>
/// Base type for a strongly-typed, parsed LemonSqueezy webhook event. Every known
/// event maps to a sealed record deriving from <see cref="LemonSqueezyWebhookEvent{TResource}"/>;
/// unrecognised event names surface as <see cref="UnknownWebhookEvent"/>. Pattern-match on the
/// concrete type to handle each event exhaustively.
/// </summary>
public abstract record LemonSqueezyWebhookEvent
{
    /// <summary>
    /// The <c>meta</c> block from the payload (event name, custom data, originating webhook id).
    /// </summary>
    public WebhookMeta Meta { get; init; } = new();

    /// <summary>
    /// Convenience accessor for <see cref="WebhookMeta.EventName"/> (e.g. <c>"order_created"</c>).
    /// </summary>
    public string? EventName => Meta.EventName;

    /// <summary>
    /// Deserializes the <c>meta.custom_data</c> bag (the arbitrary key/values attached at
    /// checkout) into <typeparamref name="TCustomData"/>. Returns <c>default</c> when no
    /// custom data was sent.
    /// </summary>
    public TCustomData? GetCustomData<TCustomData>(JsonSerializerOptions? options = null)
    {
        var customData = Meta.CustomData;
        if (customData is null || customData.Value.ValueKind is JsonValueKind.Undefined or JsonValueKind.Null)
        {
            return default;
        }

        return customData.Value.Deserialize<TCustomData>(options ?? LemonSqueezyJsonOptions.Default);
    }
}

/// <summary>
/// A strongly-typed webhook event whose <c>data</c> payload is the JSON:API resource
/// <typeparamref name="TResource"/> (for example <c>Order</c> or <c>Subscription</c>).
/// </summary>
/// <typeparam name="TResource">The resource model carried by this event.</typeparam>
public abstract record LemonSqueezyWebhookEvent<TResource> : LemonSqueezyWebhookEvent
{
    /// <summary>
    /// The deserialized resource from the event's <c>data</c> object. <c>null</c> only when the
    /// payload omitted <c>data</c>.
    /// </summary>
    public TResource? Data { get; init; }
}

/// <summary>
/// Fallback for an event name this SDK version does not recognise (for example a newly
/// introduced LemonSqueezy event). The raw <c>data</c> element is preserved so callers can
/// inspect or deserialize it themselves.
/// </summary>
public sealed record UnknownWebhookEvent : LemonSqueezyWebhookEvent
{
    /// <summary>
    /// The unparsed <c>data</c> element exactly as received.
    /// </summary>
    public JsonElement RawData { get; init; }
}
