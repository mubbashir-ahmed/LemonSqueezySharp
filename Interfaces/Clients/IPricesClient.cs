using LemonSqueezySharp.Interfaces.Clients.Base;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.Prices;

namespace LemonSqueezySharp.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/prices</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/prices.
/// </summary>
public interface IPricesClient : IApiClient
{
    /// <summary>
    /// Retrieves a price by id — <c>GET /v1/prices/{id}</c>.
    /// </summary>
    Task<BaseResult<Price>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists prices, optionally filtered by variant — <c>GET /v1/prices</c>.
    /// </summary>
    Task<PagedBaseResult<Price>> ListAsync(
        PriceListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
