using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Variants;

namespace LemonSqueezy.NET.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/variants</c> endpoints.
/// Variants are read-only — they are created and edited in the LemonSqueezy dashboard.
/// See https://docs.lemonsqueezy.com/api/variants.
/// </summary>
public interface IVariantsClient : IApiClient
{
    /// <summary>
    /// Retrieves a variant by id — <c>GET /v1/variants/{id}</c>.
    /// </summary>
    Task<BaseResult<Variant>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists variants, optionally filtered by product / status — <c>GET /v1/variants</c>.
    /// </summary>
    Task<PagedBaseResult<Variant>> ListAsync(
        VariantListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
