using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Models.OrderItems;

public sealed class OrderItem : JsonApiResource<OrderItemAttributes>
{
}

/// <summary>
/// Order-item attribute payload. See https://docs.lemonsqueezy.com/api/order-items.
/// </summary>
public sealed class OrderItemAttributes
{
}
