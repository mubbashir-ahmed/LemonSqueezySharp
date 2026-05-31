using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.SubscriptionItems;

namespace LemonSqueezy.NET.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/subscription-items</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/subscription-items.
/// </summary>
public interface ISubscriptionItemsClient : IApiClient
{
    /// <summary>
    /// Retrieves a subscription item by id — <c>GET /v1/subscription-items/{id}</c>.
    /// </summary>
    Task<BaseResult<SubscriptionItem>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the current billing period's usage for a metered subscription item
    /// — <c>GET /v1/subscription-items/{id}/current-usage</c>.
    /// </summary>
    Task<SubscriptionItemCurrentUsage?> GetCurrentUsageAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a subscription item (e.g. changing its quantity)
    /// — <c>PATCH /v1/subscription-items/{id}</c>.
    /// </summary>
    Task<BaseResult<SubscriptionItem>> UpdateAsync(
        string id,
        UpdateSubscriptionItemRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists subscription items, optionally filtered by subscription / price
    /// — <c>GET /v1/subscription-items</c>.
    /// </summary>
    Task<PagedBaseResult<SubscriptionItem>> ListAsync(
        SubscriptionItemListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
