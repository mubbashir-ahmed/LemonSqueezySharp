using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.SubscriptionItems;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ISubscriptionItemsClient"/>
public sealed class SubscriptionItemsClient : ApiClientBase, ISubscriptionItemsClient
{
    private const string ResourceType = "subscription-items";

    /// <summary>Initializes a new instance of the <see cref="SubscriptionItemsClient"/> class.</summary>
    public SubscriptionItemsClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<SubscriptionItem>> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Subscription item id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.SubscriptionItems}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<SubscriptionItem>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<SubscriptionItem>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<SubscriptionItemCurrentUsage?> GetCurrentUsageAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Subscription item id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.SubscriptionItems}/{Uri.EscapeDataString(id)}/current-usage";
        var envelope = await GetJsonAsync<SubscriptionItemCurrentUsageEnvelope>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return envelope?.Meta;
    }

    /// <inheritdoc />
    public async Task<BaseResult<SubscriptionItem>> UpdateAsync(
        string id,
        UpdateSubscriptionItemRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Subscription item id must be provided.", nameof(id));
        }

        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var body = new JsonApiWriteRequest<UpdateSubscriptionItemRequest>
        {
            Data = new JsonApiWriteData<UpdateSubscriptionItemRequest>
            {
                Type = ResourceType,
                Id = id,
                Attributes = request,
            },
        };

        var requestUri = $"{ApiEndpoints.SubscriptionItems}/{Uri.EscapeDataString(id)}";
        var response = await PatchJsonApiAsync<SubscriptionItem, JsonApiWriteRequest<UpdateSubscriptionItemRequest>>(requestUri, body, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<SubscriptionItem>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<SubscriptionItem>> ListAsync(
        SubscriptionItemListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[subscription_id]", filter.SubscriptionId);
            Append(query, "filter[price_id]", filter.PriceId);
        }

        var requestUri = $"{ApiEndpoints.SubscriptionItems}{query}";
        var response = await GetJsonApiCollectionAsync<SubscriptionItem>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<SubscriptionItem>(
            response?.Data ?? Array.Empty<SubscriptionItem>(),
            response?.Links,
            response?.Meta);
    }
}
