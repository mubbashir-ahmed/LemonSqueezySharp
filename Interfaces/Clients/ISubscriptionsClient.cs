using LemonSqueezySharp.Interfaces.Clients.Base;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.Subscriptions;

namespace LemonSqueezySharp.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/subscriptions</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/subscriptions.
/// </summary>
public interface ISubscriptionsClient : IApiClient
{
    /// <summary>
    /// Retrieves a subscription by id — <c>GET /v1/subscriptions/{id}</c>.
    /// </summary>
    Task<BaseResult<Subscription>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists subscriptions, optionally filtered by store / product / status / etc.
    /// — <c>GET /v1/subscriptions</c>.
    /// </summary>
    Task<PagedBaseResult<Subscription>> ListAsync(
        SubscriptionListFilter? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a subscription (PATCH semantics — only set the fields you want to change)
    /// — <c>PATCH /v1/subscriptions/{id}</c>. Supports plan swaps, billing-anchor
    /// changes, pause/unpause, and reactivating a cancelled subscription.
    /// </summary>
    Task<BaseResult<Subscription>> UpdateAsync(
        string id,
        UpdateSubscriptionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels a subscription — <c>DELETE /v1/subscriptions/{id}</c>. Returns the
    /// updated (cancelled) subscription resource.
    /// </summary>
    Task<BaseResult<Subscription>> CancelAsync(
        string id,
        CancellationToken cancellationToken = default);
}
