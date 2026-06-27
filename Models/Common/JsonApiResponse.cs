using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// JSON:API envelope for a single-resource response.
/// </summary>
/// <typeparam name="TResource">Strongly-typed resource (typically derived from <see cref="JsonApiResource{TAttributes}"/>).</typeparam>
public sealed class JsonApiResponse<TResource>
{
    /// <summary>
    /// The primary resource returned by the request (<c>data</c>).
    /// </summary>
    [JsonPropertyName("data")]
    public TResource? Data { get; set; }

    /// <summary>
    /// Resource-level links (<c>links</c>).
    /// </summary>
    [JsonPropertyName("links")]
    public ResourceLinks? Links { get; set; }

    /// <summary>
    /// Non-standard metadata accompanying the response (<c>meta</c>).
    /// </summary>
    [JsonPropertyName("meta")]
    public PageMeta? Meta { get; set; }
}
