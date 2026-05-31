using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// JSON:API <c>links</c> object — surfaces pagination cursors and the <c>self</c> URL.
/// </summary>
public sealed class ResourceLinks
{
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    [JsonPropertyName("first")]
    public string? First { get; set; }

    [JsonPropertyName("last")]
    public string? Last { get; set; }

    [JsonPropertyName("next")]
    public string? Next { get; set; }

    [JsonPropertyName("prev")]
    public string? Previous { get; set; }
}
