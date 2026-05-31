using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.UsageRecords;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IUsageRecordsClient"/>
public sealed class UsageRecordsClient : ApiClientBase, IUsageRecordsClient
{
    private const string ResourceType = "usage-records";
    private const string SubscriptionItemRelationship = "subscription-item";
    private const string SubscriptionItemResourceType = "subscription-items";

    public UsageRecordsClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<UsageRecord>> CreateAsync(
        CreateUsageRecordRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.SubscriptionItemId))
        {
            throw new ArgumentException("SubscriptionItemId is required.", nameof(request));
        }

        if (request.Quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(request));
        }

        var body = new JsonApiWriteRequest<UsageRecordCreateAttributes>
        {
            Data = new JsonApiWriteData<UsageRecordCreateAttributes>
            {
                Type = ResourceType,
                Attributes = new UsageRecordCreateAttributes
                {
                    Quantity = request.Quantity,
                    Action = request.Action,
                },
                Relationships = new Dictionary<string, JsonApiRelationship>
                {
                    [SubscriptionItemRelationship] =
                        JsonApiRelationship.To(SubscriptionItemResourceType, request.SubscriptionItemId),
                },
            },
        };

        var response = await PostJsonApiAsync<UsageRecord, JsonApiWriteRequest<UsageRecordCreateAttributes>>(
                ApiEndpoints.UsageRecords, body, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<UsageRecord>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<UsageRecord>> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Usage record id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.UsageRecords}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<UsageRecord>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<UsageRecord>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<UsageRecord>> ListAsync(
        UsageRecordListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[subscription_item_id]", filter.SubscriptionItemId);
        }

        var requestUri = $"{ApiEndpoints.UsageRecords}{query}";
        var response = await GetJsonApiCollectionAsync<UsageRecord>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<UsageRecord>(
            response?.Data ?? Array.Empty<UsageRecord>(),
            response?.Links,
            response?.Meta);
    }
}
