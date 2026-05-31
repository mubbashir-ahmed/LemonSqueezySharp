using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Prices;

namespace LemonSqueezy.NET.Interfaces.Clients;

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
