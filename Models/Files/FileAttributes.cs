using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Files;

/// <summary>
/// File (downloadable digital good) attribute payload.
/// See https://docs.lemonsqueezy.com/api/files.
/// </summary>
public sealed class FileAttributes
{
    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    /// <summary>Internal UUID identifier of the file.</summary>
    [JsonPropertyName("identifier")]
    public string? Identifier { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("extension")]
    public string? Extension { get; set; }

    [JsonPropertyName("download_url")]
    public string? DownloadUrl { get; set; }

    /// <summary>File size in bytes.</summary>
    [JsonPropertyName("size")]
    public long? Size { get; set; }

    [JsonPropertyName("size_formatted")]
    public string? SizeFormatted { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("sort")]
    public int? Sort { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
