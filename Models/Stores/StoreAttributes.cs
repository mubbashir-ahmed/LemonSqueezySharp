using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Stores;

/// <summary>
/// Attribute payload for a LemonSqueezy store.
/// </summary>
/// <remarks>
/// Field set is a starter — extend as endpoints are implemented.
/// See https://docs.lemonsqueezy.com/api/stores for the authoritative list.
/// </remarks>
public sealed class StoreAttributes
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
