using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Models.Products;

/// <summary>
/// Strongly-typed alias for a LemonSqueezy product resource.
/// </summary>
public sealed class Product : JsonApiResource<ProductAttributes>
{
}
