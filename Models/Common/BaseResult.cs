namespace LemonSqueezy.NET.Models.Common;

/// <summary>
/// Strongly-typed result of a single-resource endpoint — the resource itself plus the
/// JSON:API <c>links</c> and <c>meta</c> sections returned alongside it.
/// </summary>
public sealed class BaseResult<T>
{
    /// <summary>Initializes a new instance of the <see cref="BaseResult{T}"/> class.</summary>
    /// <param name="data">The resource returned by the endpoint.</param>
    /// <param name="links">The JSON:API <c>links</c> section returned alongside the resource.</param>
    /// <param name="meta">The JSON:API <c>meta</c> section returned alongside the resource.</param>
    public BaseResult(T? data, ResourceLinks? links, PageMeta? meta)
    {
        Data = data;
        Links = links;
        Meta = meta;
    }

    /// <summary>
    /// The resource returned by the endpoint.
    /// </summary>
    public T? Data { get; }

    /// <summary>
    /// The JSON:API <c>links</c> section returned alongside the resource.
    /// </summary>
    public ResourceLinks? Links { get; }

    /// <summary>
    /// The JSON:API <c>meta</c> section returned alongside the resource.
    /// </summary>
    public PageMeta? Meta { get; }
}
