namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// Strongly-typed result of a list endpoint — items plus paging cursors.
/// </summary>
public sealed class PagedBaseResult<T>
{
    public PagedBaseResult(IReadOnlyList<T> items, ResourceLinks? links, PageMeta? meta)
    {
        Items = items;
        Links = links;
        Meta = meta;
    }

    public IReadOnlyList<T> Items { get; }

    public ResourceLinks? Links { get; }

    public PageMeta? Meta { get; }

    public bool HasNext => !string.IsNullOrEmpty(Links?.Next);

    public bool HasPrevious => !string.IsNullOrEmpty(Links?.Previous);
}
