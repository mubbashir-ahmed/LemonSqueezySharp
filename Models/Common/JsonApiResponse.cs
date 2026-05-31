using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// JSON:API envelope for a single-resource response.
/// </summary>
public sealed class JsonApiResponse<TAttributes>
{
    [JsonPropertyName("data")]
    public JsonApiResource<TAttributes>? Data { get; set; }

    [JsonPropertyName("links")]
    public ResourceLinks? Links { get; set; }

    [JsonPropertyName("meta")]
    public PageMeta? Meta { get; set; }
}
