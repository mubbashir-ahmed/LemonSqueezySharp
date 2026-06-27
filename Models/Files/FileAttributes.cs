using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Files;

/// <summary>
/// File (downloadable digital good) attribute payload.
/// See https://docs.lemonsqueezy.com/api/files.
/// </summary>
public sealed class FileAttributes
{
    /// <summary>Identifier of the variant the file belongs to (<c>variant_id</c>).</summary>
    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    /// <summary>Internal UUID identifier of the file.</summary>
    [JsonPropertyName("identifier")]
    public string? Identifier { get; set; }

    /// <summary>File name (<c>name</c>).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>File extension (<c>extension</c>).</summary>
    [JsonPropertyName("extension")]
    public string? Extension { get; set; }

    /// <summary>Signed URL to download the file (<c>download_url</c>).</summary>
    [JsonPropertyName("download_url")]
    public string? DownloadUrl { get; set; }

    /// <summary>File size in bytes.</summary>
    [JsonPropertyName("size")]
    public long? Size { get; set; }

    /// <summary>Human-readable file size (<c>size_formatted</c>).</summary>
    [JsonPropertyName("size_formatted")]
    public string? SizeFormatted { get; set; }

    /// <summary>File version label (<c>version</c>).</summary>
    [JsonPropertyName("version")]
    public string? Version { get; set; }

    /// <summary>Sort order of the file within its variant (<c>sort</c>).</summary>
    [JsonPropertyName("sort")]
    public int? Sort { get; set; }

    /// <summary>Current file status (<c>status</c>).</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>Whether the file was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    /// <summary>Timestamp the file was created (<c>createdAt</c>).</summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Timestamp the file was last updated (<c>updatedAt</c>).</summary>
    [JsonPropertyName("updatedAt")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
