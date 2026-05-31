using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.OrderItems;

namespace LemonSqueezy.NET.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/order-items</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/order-items.
/// </summary>
public interface IOrderItemsClient : IApiClient
{
    /// <summary>
    /// Retrieves an order item by id — <c>GET /v1/order-items/{id}</c>.
    /// </summary>
    Task<BaseResult<OrderItem>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists order items, optionally filtered by order / product / variant
    /// — <c>GET /v1/order-items</c>.
    /// </summary>
    Task<PagedBaseResult<OrderItem>> ListAsync(
        OrderItemListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
