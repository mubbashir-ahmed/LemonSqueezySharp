using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Prices;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IPricesClient"/>
public sealed class PricesClient : ApiClientBase, IPricesClient
{
    /// <summary>Initializes a new instance of the <see cref="PricesClient"/> class.</summary>
    public PricesClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<Price>> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Price id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Prices}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<Price>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Price>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<Price>> ListAsync(
        PriceListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[variant_id]", filter.VariantId);
        }

        var requestUri = $"{ApiEndpoints.Prices}{query}";
        var response = await GetJsonApiCollectionAsync<Price>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<Price>(
            response?.Data ?? Array.Empty<Price>(),
            response?.Links,
            response?.Meta);
    }
}
