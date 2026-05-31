using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Products;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IProductsClient"/>
public sealed class ProductsClient : ApiClientBase, IProductsClient
{
    public ProductsClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<Product>> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Product id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Products}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<Product>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Product>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<Product>> ListAsync(
        ProductListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[store_id]", filter.StoreId);
        }

        var requestUri = $"{ApiEndpoints.Products}{query.ToString()}";
        var response = await GetJsonApiCollectionAsync<Product>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<Product>(
            response?.Data ?? Array.Empty<Product>(),
            response?.Links,
            response?.Meta);
    }
}
