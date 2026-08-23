using LemonSqueezySharp.Clients.Base;
using LemonSqueezySharp.Http;
using LemonSqueezySharp.Interfaces.Clients;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.Variants;

namespace LemonSqueezySharp.Clients;

/// <inheritdoc cref="IVariantsClient"/>
public sealed class VariantsClient : ApiClientBase, IVariantsClient
{
    /// <summary>Initializes a new instance of the <see cref="VariantsClient"/> class.</summary>
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
