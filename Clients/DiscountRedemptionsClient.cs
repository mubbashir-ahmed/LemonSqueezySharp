using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.DiscountRedemptions;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IDiscountRedemptionsClient"/>
public sealed class DiscountRedemptionsClient : ApiClientBase, IDiscountRedemptionsClient
{
    /// <summary>Initializes a new instance of the <see cref="DiscountRedemptionsClient"/> class.</summary>
    public DiscountRedemptionsClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<DiscountRedemption>> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Discount redemption id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.DiscountRedemptions}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<DiscountRedemption>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<DiscountRedemption>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<DiscountRedemption>> ListAsync(
        DiscountRedemptionListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[discount_id]", filter.DiscountId);
            Append(query, "filter[order_id]", filter.OrderId);
        }

        var requestUri = $"{ApiEndpoints.DiscountRedemptions}{query}";
        var response = await GetJsonApiCollectionAsync<DiscountRedemption>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<DiscountRedemption>(
            response?.Data ?? Array.Empty<DiscountRedemption>(),
            response?.Links,
            response?.Meta);
    }
}
