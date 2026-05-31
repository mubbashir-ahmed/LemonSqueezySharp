using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Webhooks;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IWebhooksClient"/>
public sealed class WebhooksClient : ApiClientBase, IWebhooksClient
{
    private const string ResourceType = "webhooks";
    private const string StoreRelationship = "store";
    private const string StoreResourceType = "stores";

    public WebhooksClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<Webhook>> CreateAsync(
        CreateWebhookRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.StoreId))
        {
            throw new ArgumentException("StoreId is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Url))
        {
            throw new ArgumentException("Url is required.", nameof(request));
        }

        if (request.Events is null || request.Events.Count == 0)
        {
            throw new ArgumentException("Events must contain at least one entry.", nameof(request));
        }

        var body = new JsonApiWriteRequest<WebhookCreateAttributes>
        {
            Data = new JsonApiWriteData<WebhookCreateAttributes>
            {
                Type = ResourceType,
                Attributes = new WebhookCreateAttributes
                {
                    Url = request.Url,
                    Events = request.Events,
                    Secret = request.Secret,
                },
                Relationships = new Dictionary<string, JsonApiRelationship>
                {
                    [StoreRelationship] = JsonApiRelationship.To(StoreResourceType, request.StoreId),
                },
            },
        };

        var requestUri = $"{ApiEndpoints.Webhooks}";
        var response = await PostJsonApiAsync<Webhook, JsonApiWriteRequest<WebhookCreateAttributes>>(requestUri, body, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Webhook>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<Webhook>> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Webhook id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Webhooks}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<Webhook>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Webhook>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<Webhook>> UpdateAsync(
        string id,
        UpdateWebhookRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Webhook id must be provided.", nameof(id));
        }

        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var body = new JsonApiWriteRequest<WebhookUpdateAttributes>
        {
            Data = new JsonApiWriteData<WebhookUpdateAttributes>
            {
                Type = ResourceType,
                Id = id,
                Attributes = new WebhookUpdateAttributes
                {
                    Url = request.Url,
                    Events = request.Events,
                    Secret = request.Secret,
                },
            },
        };

        var requestUri = $"{ApiEndpoints.Webhooks}/{Uri.EscapeDataString(id)}";
        var response = await PatchJsonApiAsync<Webhook, JsonApiWriteRequest<WebhookUpdateAttributes>>(
                requestUri, body, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Webhook>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Webhook id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Webhooks}/{Uri.EscapeDataString(id)}";
        await DeleteJsonAsync(requestUri, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<Webhook>> ListAsync(
        WebhookListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[store_id]", filter.StoreId);
        }

        var requestUri = $"{ApiEndpoints.Webhooks}{query}";
        var response = await GetJsonApiCollectionAsync<Webhook>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<Webhook>(
            response?.Data ?? Array.Empty<Webhook>(),
            response?.Links,
            response?.Meta);
    }
}
