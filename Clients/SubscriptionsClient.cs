using LemonSqueezySharp.Clients.Base;
using LemonSqueezySharp.Http;
using LemonSqueezySharp.Interfaces.Clients;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.Subscriptions;

namespace LemonSqueezySharp.Clients;

/// <inheritdoc cref="ISubscriptionsClient"/>
public sealed class SubscriptionsClient : ApiClientBase, ISubscriptionsClient
{
    private const string ResourceType = "subscriptions";

    /// <summary>Initializes a new instance of the <see cref="SubscriptionsClient"/> class.</summary>
    public SubscriptionsClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<Subscription>> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Subscription id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Subscriptions}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<Subscription>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Subscription>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<Subscription>> ListAsync(
        SubscriptionListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[store_id]", filter.StoreId);
            Append(query, "filter[order_id]", filter.OrderId);
            Append(query, "filter[order_item_id]", filter.OrderItemId);
            Append(query, "filter[product_id]", filter.ProductId);
            Append(query, "filter[variant_id]", filter.VariantId);
            Append(query, "filter[user_email]", filter.UserEmail);
            Append(query, "filter[status]", filter.Status);
        }

        var requestUri = $"{ApiEndpoints.Subscriptions}{query}";
        var response = await GetJsonApiCollectionAsync<Subscription>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<Subscription>(
            response?.Data ?? Array.Empty<Subscription>(),
            response?.Links,
            response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<Subscription>> UpdateAsync(
        string id,
        UpdateSubscriptionRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Subscription id must be provided.", nameof(id));
        }

        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var body = new JsonApiWriteRequest<UpdateSubscriptionRequest>
        {
            Data = new JsonApiWriteData<UpdateSubscriptionRequest>
            {
                Type = ResourceType,
                Id = id,
                Attributes = request,
            },
        };

        var requestUri = $"{ApiEndpoints.Subscriptions}/{Uri.EscapeDataString(id)}";
        var response = await PatchJsonApiAsync<Subscription, JsonApiWriteRequest<UpdateSubscriptionRequest>>(requestUri, body, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Subscription>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<Subscription>> CancelAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Subscription id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Subscriptions}/{Uri.EscapeDataString(id)}";
        var response = await DeleteJsonApiAsync<Subscription>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Subscription>(response?.Data, response?.Links, response?.Meta);
    }
}
