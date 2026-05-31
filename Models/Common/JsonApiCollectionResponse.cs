using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// JSON:API envelope for a multi-resource (collection) response.
/// </summary>
/// <typeparam name="TResource">Strongly-typed resource (typically derived from <see cref="JsonApiResource{TAttributes}"/>).</typeparam>
public sealed class JsonApiCollectionResponse<TResource>
{
    [JsonPropertyName("data")]
    public IReadOnlyList<TResource> Data { get; set; } = Array.Empty<TResource>();

    [JsonPropertyName("links")]
    public ResourceLinks? Links { get; set; }

    [JsonPropertyName("meta")]
    public PageMeta? Meta { get; set; }
}
