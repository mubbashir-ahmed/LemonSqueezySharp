using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Affiliates;
using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/affiliates</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/affiliates.
/// </summary>
public interface IAffiliatesClient : IApiClient
{
    /// <summary>
    /// Retrieves an affiliate by id — <c>GET /v1/affiliates/{id}</c>.
    /// </summary>
    Task<BaseResult<Affiliate>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists affiliates, optionally filtered by store — <c>GET /v1/affiliates</c>.
    /// </summary>
    Task<PagedBaseResult<Affiliate>> ListAsync(
        AffiliateListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
