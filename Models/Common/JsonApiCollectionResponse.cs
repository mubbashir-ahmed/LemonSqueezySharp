using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// JSON:API envelope for a multi-resource (collection) response.
/// </summary>
/// <typeparam name="TResource">Strongly-typed resource (typically derived from <see cref="JsonApiResource{TAttributes}"/>).</typeparam>
public sealed class JsonApiCollectionResponse<TResource>
{
    /// <summary>
    /// The collection of resources returned by the request (<c>data</c>).
    /// </summary>
    [JsonPropertyName("data")]
    public IReadOnlyList<TResource> Data { get; set; } = Array.Empty<TResource>();

    /// <summary>
    /// Collection-level links, including pagination cursors (<c>links</c>).
    /// </summary>
    [JsonPropertyName("links")]
    public ResourceLinks? Links { get; set; }

    /// <summary>
    /// Non-standard metadata accompanying the response, including paging info (<c>meta</c>).
    /// </summary>
    [JsonPropertyName("meta")]
    public PageMeta? Meta { get; set; }
}
