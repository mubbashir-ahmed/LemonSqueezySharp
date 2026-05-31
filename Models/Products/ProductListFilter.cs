using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.Products;

/// <summary>
/// Filter for <c>GET /v1/products</c>. Extends the standard <see cref="ListFilter"/>
/// with product-specific server-side filters.
/// </summary>
public sealed class ProductListFilter : ListFilter
{
    /// <summary>
    /// Filter products to the supplied store id (<c>filter[store_id]</c>).
    /// </summary>
    public string? StoreId { get; set; }
}
