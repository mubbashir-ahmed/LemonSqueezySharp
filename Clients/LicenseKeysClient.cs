using LemonSqueezySharp.Clients.Base;
using LemonSqueezySharp.Http;
using LemonSqueezySharp.Interfaces.Clients;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.LicenseKeys;

namespace LemonSqueezySharp.Clients;

/// <inheritdoc cref="ILicenseKeysClient"/>
public sealed class LicenseKeysClient : ApiClientBase, ILicenseKeysClient
{
    private const string ResourceType = "license-keys";

    /// <summary>Initializes a new instance of the <see cref="LicenseKeysClient"/> class.</summary>
    public LicenseKeysClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<LicenseKey>> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("License key id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.LicenseKeys}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<LicenseKey>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<LicenseKey>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<LicenseKey>> UpdateAsync(
        string id,
        UpdateLicenseKeyRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("License key id must be provided.", nameof(id));
        }

        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var body = new JsonApiWriteRequest<LicenseKeyUpdateAttributes>
        {
            Data = new JsonApiWriteData<LicenseKeyUpdateAttributes>
            {
                Type = ResourceType,
                Id = id,
                Attributes = new LicenseKeyUpdateAttributes
                {
                    ActivationLimit = request.ActivationLimit,
                    Disabled = request.Disabled,
                    ExpiresAt = request.ExpiresAt,
                },
            },
        };

        var requestUri = $"{ApiEndpoints.LicenseKeys}/{Uri.EscapeDataString(id)}";
        var response = await PatchJsonApiAsync<LicenseKey, JsonApiWriteRequest<LicenseKeyUpdateAttributes>>(
                requestUri, body, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<LicenseKey>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<LicenseKey>> ListAsync(
        LicenseKeyListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[store_id]", filter.StoreId);
            Append(query, "filter[order_id]", filter.OrderId);
            Append(query, "filter[order_item_id]", filter.OrderItemId);
            Append(query, "filter[product_id]", filter.ProductId);
            Append(query, "filter[status]", filter.Status);
        }

        var requestUri = $"{ApiEndpoints.LicenseKeys}{query}";
        var response = await GetJsonApiCollectionAsync<LicenseKey>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<LicenseKey>(
            response?.Data ?? Array.Empty<LicenseKey>(),
            response?.Links,
            response?.Meta);
    }
}
