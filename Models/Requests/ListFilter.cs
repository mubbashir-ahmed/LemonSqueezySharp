namespace LemonSqueezySharp.Models.Requests;

/// <summary>
/// Common filter/page/include parameters shared across LemonSqueezy list endpoints.
/// Each resource client will derive its own typed filter on top of this.
/// </summary>
public class ListFilter
{
    /// <summary>
    /// 1-indexed page number (JSON:API <c>page[number]</c>).
    /// </summary>
    public int? PageNumber { get; set; }

    /// <summary>
    /// Page size (JSON:API <c>page[size]</c>). Server caps typically apply.
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// Comma-separated list of related resources to side-load (<c>include</c>).
    /// </summary>
    public string? Include { get; set; }

    /// <summary>
    /// Sort expression for list endpoints (JSON:API <c>sort</c>).
    /// A leading <c>-</c> sorts descending, e.g. <c>-created_at</c> (newest first).
    /// Common values: <c>created_at</c>, <c>updated_at</c>, <c>price</c>, <c>name</c>.
    /// </summary>
    public string? Sort { get; set; }
}
