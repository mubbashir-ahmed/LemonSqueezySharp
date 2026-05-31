using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// A single JSON:API resource object with a strongly-typed attributes payload.
/// </summary>
/// <typeparam name="TAttributes">Resource-specific attribute shape.</typeparam>
public class JsonApiResource<TAttributes>
{
    /// <summary>
    /// Resource id (string per JSON:API spec).
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Resource type discriminator (e.g. <c>"subscriptions"</c>).
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Strongly-typed attribute payload.
    /// </summary>
    [JsonPropertyName("attributes")]
    public TAttributes? Attributes { get; set; }

    /// <summary>
    /// Resource-level links (<c>self</c> etc.).
    /// </summary>
    [JsonPropertyName("links")]
    public ResourceLinks? Links { get; set; }
}
