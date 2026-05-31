using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// JSON:API <c>relationships</c> entry — wraps a single resource identifier
/// (e.g. <c>{"data": {"type": "stores", "id": "1"}}</c>).
/// </summary>
public sealed class JsonApiRelationship
{
    [JsonPropertyName("data")]
    public JsonApiRelationshipIdentifier? Data { get; set; }

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
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}
