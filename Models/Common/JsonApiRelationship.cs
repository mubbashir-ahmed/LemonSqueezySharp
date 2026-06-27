using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// JSON:API <c>relationships</c> entry — wraps a single resource identifier
/// (e.g. <c>{"data": {"type": "stores", "id": "1"}}</c>).
/// </summary>
public sealed class JsonApiRelationship
{
    /// <summary>
    /// The resource identifier this relationship points at (<c>data</c>).
    /// </summary>
    [JsonPropertyName("data")]
    public JsonApiRelationshipIdentifier? Data { get; set; }

    /// <summary>
    /// Creates a relationship referencing a single resource by type and id.
    /// </summary>
    /// <param name="type">Resource type discriminator (e.g. <c>"stores"</c>).</param>
    /// <param name="id">Resource id.</param>
    /// <returns>A <see cref="JsonApiRelationship"/> wrapping the given identifier.</returns>
    public static JsonApiRelationship To(string type, string id) => new()
    {
        Data = new JsonApiRelationshipIdentifier { Type = type, Id = id },
    };
}

/// <summary>
/// JSON:API resource identifier — the <c>{type, id}</c> pair used inside relationships.
/// </summary>
public sealed class JsonApiRelationshipIdentifier
{
    /// <summary>
    /// Resource type discriminator (<c>type</c>).
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Resource id (<c>id</c>).
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}
