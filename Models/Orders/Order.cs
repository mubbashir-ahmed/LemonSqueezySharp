using LemonSqueezySharp.Models.Common;

namespace LemonSqueezySharp.Models.Orders;

/// <summary>
/// Strongly-typed alias for a LemonSqueezy order resource.
/// </summary>
public sealed class Order : JsonApiResource<OrderAttributes>
{
}
