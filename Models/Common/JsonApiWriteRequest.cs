using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.Common;

/// <summary>
/// JSON:API envelope used for <c>POST</c> / <c>PATCH</c> bodies.
/// </summary>
/// <typeparam name="TAttributes">Strongly-typed resource attributes payload.</typeparam>
public sealed class JsonApiWriteRequest<TAttributes>
{
    /// <summary>
    /// The JSON:API <c>data</c> block carrying the resource being written.
    /// </summary>
    [JsonPropertyName("data")]
    public JsonApiWriteData<TAttributes> Data { get; set; } = new();
}

/// <summary>
/// JSON:API <c>data</c> block used in write requests.
/// </summary>
public sealed class JsonApiWriteData<TAttributes>
{
    /// <summary>
    /// Resource type discriminator — e.g. <c>"customers"</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Resource id. Required on <c>PATCH</c>, omitted on <c>POST</c>.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Strongly-typed attribute payload.
    /// </summary>
    [JsonPropertyName("attributes")]
    public TAttributes? Attributes { get; set; }

    /// <summary>
    /// Optional relationships block, keyed by relationship name (e.g. <c>store</c>).
    /// </summary>
    [JsonPropertyName("relationships")]
    public Dictionary<string, JsonApiRelationship>? Relationships { get; set; }
}
