using LemonSqueezySharp.Clients.Base;
using LemonSqueezySharp.Http;
using LemonSqueezySharp.Interfaces.Clients;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.Products;

namespace LemonSqueezySharp.Clients;

/// <inheritdoc cref="IProductsClient"/>
public sealed class ProductsClient : ApiClientBase, IProductsClient
{
    /// <summary>Initializes a new instance of the <see cref="ProductsClient"/> class.</summary>
    public ProductsClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<Product>> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
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

        var requestUri = $"{ApiEndpoints.Products}{query}";
        var response = await GetJsonApiCollectionAsync<Product>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<Product>(
            response?.Data ?? Array.Empty<Product>(),
            response?.Links,
            response?.Meta);
    }
}
