using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// Pagination metadata typically returned under <c>meta.page</c> by LemonSqueezy.
/// </summary>
public sealed class PageMeta
{
    [JsonPropertyName("page")]
    public PageInfo? Page { get; set; }
}

/// <summary>
/// Page-level counts and cursors.
/// </summary>
public sealed class PageInfo
{
    [JsonPropertyName("number")]
    public int? Number { get; set; }

    [JsonPropertyName("size")]
    public int? Size { get; set; }

    [JsonPropertyName("total")]
    public int? Total { get; set; }

    [JsonPropertyName("lastPage")]
    public int? LastPage { get; set; }
}
