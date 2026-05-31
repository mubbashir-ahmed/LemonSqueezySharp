using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.Files;

/// <summary>
/// Filter for <c>GET /v1/files</c>.
/// </summary>
public sealed class FileListFilter : ListFilter
{
    /// <summary>
    /// Filter files by variant id (<c>filter[variant_id]</c>).
    /// </summary>
    public string? VariantId { get; set; }
}
