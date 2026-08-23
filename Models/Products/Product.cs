using LemonSqueezySharp.Models.Common;

namespace LemonSqueezySharp.Models.Products;

/// <summary>
/// Strongly-typed alias for a LemonSqueezy product resource.
/// </summary>
public sealed class Product : JsonApiResource<ProductAttributes>
{
}
