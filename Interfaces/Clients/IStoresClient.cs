using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Requests;
using LemonSqueezy.NET.Models.Stores;

namespace LemonSqueezy.NET.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/stores</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/stores.
/// </summary>
public interface IStoresClient : IApiClient
{
    /// <summary>
    /// Retrieves a store by id — <c>GET /v1/stores/{id}</c>.
    /// </summary>
    /// <param name="id">The store resource id.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    Task<BaseResult<Store>> GetAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists every store on the account — <c>GET /v1/stores</c>.
    /// Supports <c>page[number]</c>, <c>page[size]</c>, and <c>include</c> via
    /// <paramref name="filter"/>.
    /// </summary>
    /// <param name="filter">Optional paging / include filter.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    Task<PagedBaseResult<Store>> ListAsync(
        ListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
