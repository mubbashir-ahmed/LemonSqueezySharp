using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Products;

namespace LemonSqueezy.NET.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/products</c> endpoints.
/// Products are read-only — they are created and edited in the LemonSqueezy dashboard.
/// See https://docs.lemonsqueezy.com/api/products.
/// </summary>
public interface IProductsClient : IApiClient
{
    /// <summary>
    /// Retrieves a product by id — <c>GET /v1/products/{id}</c>.
    /// </summary>
    Task<BaseResult<Product>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists products, optionally filtered by store and paged — <c>GET /v1/products</c>.
    /// </summary>
    Task<PagedBaseResult<Product>> ListAsync(
        ProductListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
