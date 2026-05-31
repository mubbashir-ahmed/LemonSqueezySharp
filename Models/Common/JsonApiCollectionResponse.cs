using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// JSON:API envelope for a multi-resource (collection) response.
/// </summary>
public sealed class JsonApiCollectionResponse<TAttributes>
{
    [JsonPropertyName("data")]
    public IReadOnlyList<JsonApiResource<TAttributes>> Data { get; set; } = Array.Empty<JsonApiResource<TAttributes>>();

    [JsonPropertyName("links")]
    public ResourceLinks? Links { get; set; }

    [JsonPropertyName("meta")]
    public PageMeta? Meta { get; set; }
}
