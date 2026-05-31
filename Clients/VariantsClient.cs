using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Variants;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IVariantsClient"/>
public sealed class VariantsClient : ApiClientBase, IVariantsClient
{
    public VariantsClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<Variant>> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Variant id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Variants}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<Variant>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Variant>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<Variant>> ListAsync(
        VariantListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[product_id]", filter.ProductId);
            Append(query, "filter[status]", filter.Status);
        }

        var requestUri = $"{ApiEndpoints.Variants}{query}";
        var response = await GetJsonApiCollectionAsync<Variant>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<Variant>(
            response?.Data ?? Array.Empty<Variant>(),
            response?.Links,
            response?.Meta);
    }
}
