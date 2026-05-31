using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// JSON:API envelope for a single-resource response.
/// </summary>
/// <typeparam name="TResource">Strongly-typed resource (typically derived from <see cref="JsonApiResource{TAttributes}"/>).</typeparam>
public sealed class JsonApiResponse<TResource>
{
    [JsonPropertyName("data")]
    public TResource? Data { get; set; }

    [JsonPropertyName("links")]
    public ResourceLinks? Links { get; set; }

    [JsonPropertyName("meta")]
    public PageMeta? Meta { get; set; }
}
