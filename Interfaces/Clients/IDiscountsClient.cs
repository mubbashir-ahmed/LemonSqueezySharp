using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Discounts;

namespace LemonSqueezy.NET.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/discounts</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/discounts.
/// </summary>
public interface IDiscountsClient : IApiClient
{
    /// <summary>
    /// Creates a new discount — <c>POST /v1/discounts</c>.
    /// </summary>
    Task<BaseResult<Discount>> CreateAsync(
        CreateDiscountRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a discount by id — <c>GET /v1/discounts/{id}</c>.
    /// </summary>
    Task<BaseResult<Discount>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Permanently deletes a discount — <c>DELETE /v1/discounts/{id}</c>.
    /// Returns <c>204 No Content</c>; this task completes successfully on a 204.
    /// </summary>
    Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists discounts, optionally filtered by store — <c>GET /v1/discounts</c>.
    /// </summary>
    Task<PagedBaseResult<Discount>> ListAsync(
        DiscountListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
