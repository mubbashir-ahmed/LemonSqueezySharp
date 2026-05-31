using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Checkouts;
using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ICheckoutsClient"/>
public sealed class CheckoutsClient : ApiClientBase, ICheckoutsClient
{
    private const string ResourceType = "checkouts";
    private const string StoreRelationship = "store";
    private const string VariantRelationship = "variant";
    private const string StoreResourceType = "stores";
    private const string VariantResourceType = "variants";

    public CheckoutsClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<Checkout>> CreateAsync(
        CreateCheckoutRequest request,
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

        if (string.IsNullOrWhiteSpace(request.VariantId))
        {
            throw new ArgumentException("VariantId is required.", nameof(request));
        }

        var body = new JsonApiWriteRequest<CheckoutCreateAttributes>
        {
            Data = new JsonApiWriteData<CheckoutCreateAttributes>
            {
                Type = ResourceType,
                Attributes = new CheckoutCreateAttributes
                {
                    CustomPrice = request.CustomPrice,
                    ProductOptions = request.ProductOptions,
                    CheckoutOptions = request.CheckoutOptions,
                    CheckoutData = request.CheckoutData,
                    Preview = request.Preview,
                    ExpiresAt = request.ExpiresAt,
                    TestMode = request.TestMode,
                },
                Relationships = new Dictionary<string, JsonApiRelationship>
                {
                    [StoreRelationship] = JsonApiRelationship.To(StoreResourceType, request.StoreId),
                    [VariantRelationship] = JsonApiRelationship.To(VariantResourceType, request.VariantId),
                },
            },
        };

        var requestUri = $"{ApiEndpoints.Checkouts}";
        var response = await PostJsonApiAsync<Checkout, JsonApiWriteRequest<CheckoutCreateAttributes>>(requestUri, body, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Checkout>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<Checkout>> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Checkout id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Checkouts}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<Checkout>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Checkout>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<Checkout>> ListAsync(
        CheckoutListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[store_id]", filter.StoreId);
            Append(query, "filter[variant_id]", filter.VariantId);
        }

        var requestUri = $"{ApiEndpoints.Checkouts}{query}";
        var response = await GetJsonApiCollectionAsync<Checkout>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<Checkout>(
            response?.Data ?? Array.Empty<Checkout>(),
            response?.Links,
            response?.Meta);
    }
}
