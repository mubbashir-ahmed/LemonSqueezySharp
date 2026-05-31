using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Discounts;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IDiscountsClient"/>
public sealed class DiscountsClient : ApiClientBase, IDiscountsClient
{
    private const string ResourceType = "discounts";
    private const string StoreRelationship = "store";
    private const string StoreResourceType = "stores";

    public DiscountsClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<Discount>> CreateAsync(
        CreateDiscountRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Code))
        {
            throw new ArgumentException("Code is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.AmountType))
        {
            throw new ArgumentException("AmountType is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.StoreId))
        {
            throw new ArgumentException("StoreId is required.", nameof(request));
        }

        var body = new JsonApiWriteRequest<DiscountCreateAttributes>
        {
            Data = new JsonApiWriteData<DiscountCreateAttributes>
            {
                Type = ResourceType,
                Attributes = new DiscountCreateAttributes
                {
                    Name = request.Name,
                    Code = request.Code,
                    Amount = request.Amount,
                    AmountType = request.AmountType,
                    Duration = request.Duration,
                    DurationInMonths = request.DurationInMonths,
                    StartsAt = request.StartsAt,
                    ExpiresAt = request.ExpiresAt,
                    IsLimitedRedemptions = request.IsLimitedRedemptions,
                    MaxRedemptions = request.MaxRedemptions,
                },
                Relationships = new Dictionary<string, JsonApiRelationship>
                {
                    [StoreRelationship] = JsonApiRelationship.To(StoreResourceType, request.StoreId),
                },
            },
        };

        var requestUri = $"{ApiEndpoints.Discounts}";
        var response = await PostJsonApiAsync<Discount, JsonApiWriteRequest<DiscountCreateAttributes>>(requestUri, body, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Discount>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<Discount>> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Discount id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Discounts}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<Discount>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Discount>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Discount id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Discounts}/{Uri.EscapeDataString(id)}";
        await base.DeleteJsonAsync(requestUri, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<Discount>> ListAsync(
        DiscountListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[store_id]", filter.StoreId);
        }

        var requestUri = $"{ApiEndpoints.Discounts}{query}";
        var response = await GetJsonApiCollectionAsync<Discount>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<Discount>(
            response?.Data ?? Array.Empty<Discount>(),
            response?.Links,
            response?.Meta);
    }
}
