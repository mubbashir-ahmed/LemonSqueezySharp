using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// Pagination metadata typically returned under <c>meta.page</c> by LemonSqueezy.
/// </summary>
public sealed class PageMeta
{
    /// <summary>
    /// Page-level counts and cursors (<c>page</c>).
    /// </summary>
    [JsonPropertyName("page")]
    public PageInfo? Page { get; set; }
}

/// <summary>
/// Page-level counts and cursors.
/// </summary>
public sealed class PageInfo
{
    /// <summary>
    /// Current 1-indexed page number (<c>number</c>).
    /// </summary>
    [JsonPropertyName("number")]
    public int? Number { get; set; }

    /// <summary>
    /// Number of items per page (<c>size</c>).
    /// </summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }

    /// <summary>
    /// Total number of items across all pages (<c>total</c>).
    /// </summary>
    [JsonPropertyName("total")]
    public int? Total { get; set; }

    /// <summary>
    /// Number of the last available page (<c>lastPage</c>).
    /// </summary>
    [JsonPropertyName("lastPage")]
    public int? LastPage { get; set; }
}
