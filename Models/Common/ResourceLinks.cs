using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.Common;

/// <summary>
/// JSON:API <c>links</c> object — surfaces pagination cursors and the <c>self</c> URL.
/// </summary>
public sealed class ResourceLinks
{
    /// <summary>
    /// URL of the current resource or page (<c>self</c>).
    /// </summary>
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    /// <summary>
    /// URL of the first page of the collection (<c>first</c>).
    /// </summary>
    [JsonPropertyName("first")]
    public string? First { get; set; }

    /// <summary>
    /// URL of the last page of the collection (<c>last</c>).
    /// </summary>
    [JsonPropertyName("last")]
    public string? Last { get; set; }

    /// <summary>
    /// URL of the next page of the collection (<c>next</c>), if any.
    /// </summary>
    [JsonPropertyName("next")]
    public string? Next { get; set; }

    /// <summary>
    /// URL of the previous page of the collection (<c>prev</c>), if any.
    /// </summary>
    [JsonPropertyName("prev")]
    public string? Previous { get; set; }
}
