using LemonSqueezySharp.Clients.Base;
using LemonSqueezySharp.Http;
using LemonSqueezySharp.Interfaces.Clients;
using LemonSqueezySharp.Models.Affiliates;
using LemonSqueezySharp.Models.Common;

namespace LemonSqueezySharp.Clients;

/// <inheritdoc cref="IAffiliatesClient"/>
public sealed class AffiliatesClient : ApiClientBase, IAffiliatesClient
{
    /// <summary>Initializes a new instance of the <see cref="AffiliatesClient"/> class.</summary>
    public AffiliatesClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<Affiliate>> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Affiliate id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Affiliates}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<Affiliate>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Affiliate>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<Affiliate>> ListAsync(
        AffiliateListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[store_id]", filter.StoreId);
        }

        var requestUri = $"{ApiEndpoints.Affiliates}{query}";
        var response = await GetJsonApiCollectionAsync<Affiliate>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<Affiliate>(
            response?.Data ?? Array.Empty<Affiliate>(),
            response?.Links,
            response?.Meta);
    }
}
