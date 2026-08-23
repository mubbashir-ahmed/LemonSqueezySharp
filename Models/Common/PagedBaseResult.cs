namespace LemonSqueezySharp.Models.Common;

/// <summary>
/// Strongly-typed result of a list endpoint — items plus paging cursors.
/// </summary>
public sealed class PagedBaseResult<T>
{
    /// <summary>Initializes a new instance of the <see cref="PagedBaseResult{T}"/> class.</summary>
    /// <param name="items">The items in the current page.</param>
    /// <param name="links">The JSON:API <c>links</c> section, including paging cursors.</param>
    /// <param name="meta">The JSON:API <c>meta</c> section, including paging counts.</param>
    public PagedBaseResult(IReadOnlyList<T> items, ResourceLinks? links, PageMeta? meta)
    {
        Items = items;
        Links = links;
        Meta = meta;
    }

    /// <summary>
    /// The items in the current page.
    /// </summary>
    public IReadOnlyList<T> Items { get; }

    /// <summary>
    /// The JSON:API <c>links</c> section, including paging cursors.
    /// </summary>
    public ResourceLinks? Links { get; }

    /// <summary>
    /// The JSON:API <c>meta</c> section, including paging counts.
    /// </summary>
    public PageMeta? Meta { get; }

    /// <summary>
    /// Gets a value indicating whether a next page is available.
    /// </summary>
    public bool HasNext => !string.IsNullOrWhiteSpace(Links?.Next);

    /// <summary>
    /// Gets a value indicating whether a previous page is available.
    /// </summary>
    public bool HasPrevious => !string.IsNullOrWhiteSpace(Links?.Previous);
}
