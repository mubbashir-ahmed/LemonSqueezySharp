namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// Strongly-typed result of a single-resource endpoint — the resource itself plus the
/// JSON:API <c>links</c> and <c>meta</c> sections returned alongside it.
/// </summary>
public sealed class BaseResult<T>
{
    public BaseResult(T? data, ResourceLinks? links, PageMeta? meta)
    {
        Data = data;
        Links = links;
        Meta = meta;
    }

    public T? Data { get; }

    public ResourceLinks? Links { get; }

    public PageMeta? Meta { get; }
}
