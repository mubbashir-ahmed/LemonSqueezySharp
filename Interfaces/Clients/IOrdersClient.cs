using LemonSqueezySharp.Interfaces.Clients.Base;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.Orders;

namespace LemonSqueezySharp.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/orders</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/orders.
/// </summary>
public interface IOrdersClient : IApiClient
{
    /// <summary>
    /// Retrieves an order by id — <c>GET /v1/orders/{id}</c>.
    /// </summary>
    Task<BaseResult<Order>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists orders, optionally filtered by store / email — <c>GET /v1/orders</c>.
    /// </summary>
    Task<PagedBaseResult<Order>> ListAsync(
        OrderListFilter? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates an invoice PDF for the order and returns its short-lived URL
    /// — <c>POST /v1/orders/{id}/generate-invoice</c>. Optional address overrides
    /// are passed as query-string parameters.
    /// </summary>
    Task<BaseResult<OrderInvoice>> GenerateInvoiceAsync(
        string id,
        GenerateOrderInvoiceRequest? request = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Issues a refund (full or partial) for an order
    /// — <c>POST /v1/orders/{id}/refund</c>. Returns the updated order.
    /// </summary>
    Task<BaseResult<Order>> RefundAsync(
        string id,
        RefundOrderRequest? request = null,
        CancellationToken cancellationToken = default);
}
