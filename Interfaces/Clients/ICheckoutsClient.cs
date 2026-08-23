using LemonSqueezySharp.Interfaces.Clients.Base;
using LemonSqueezySharp.Models.Checkouts;
using LemonSqueezySharp.Models.Common;

namespace LemonSqueezySharp.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/checkouts</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/checkouts.
/// </summary>
public interface ICheckoutsClient : IApiClient
{
    /// <summary>
    /// Creates a new checkout — <c>POST /v1/checkouts</c>. The response's
    /// <c>attributes.url</c> is the hosted-checkout link to redirect the customer to.
    /// </summary>
    Task<BaseResult<Checkout>> CreateAsync(
        CreateCheckoutRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a checkout by id — <c>GET /v1/checkouts/{id}</c>.
    /// </summary>
    Task<BaseResult<Checkout>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists checkouts, optionally filtered by store / variant
    /// — <c>GET /v1/checkouts</c>.
    /// </summary>
    Task<PagedBaseResult<Checkout>> ListAsync(
        CheckoutListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
