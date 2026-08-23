using LemonSqueezySharp.Interfaces.Clients.Base;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.DiscountRedemptions;

namespace LemonSqueezySharp.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/discount-redemptions</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/discount-redemptions.
/// </summary>
public interface IDiscountRedemptionsClient : IApiClient
{
    /// <summary>
    /// Retrieves a discount redemption by id — <c>GET /v1/discount-redemptions/{id}</c>.
    /// </summary>
    Task<BaseResult<DiscountRedemption>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists discount redemptions, optionally filtered by discount / order
    /// — <c>GET /v1/discount-redemptions</c>.
    /// </summary>
    Task<PagedBaseResult<DiscountRedemption>> ListAsync(
        DiscountRedemptionListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
