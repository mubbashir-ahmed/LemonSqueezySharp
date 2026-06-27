using System.Text;
using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Orders;
using LemonSqueezy.NET.Models.SubscriptionInvoices;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ISubscriptionInvoicesClient"/>
public sealed class SubscriptionInvoicesClient : ApiClientBase, ISubscriptionInvoicesClient
{
    private const string ResourceType = "subscription-invoices";

    /// <summary>Initializes a new instance of the <see cref="SubscriptionInvoicesClient"/> class.</summary>
    public SubscriptionInvoicesClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<SubscriptionInvoice>> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Subscription invoice id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.SubscriptionInvoices}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<SubscriptionInvoice>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<SubscriptionInvoice>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<SubscriptionInvoice>> ListAsync(
        SubscriptionInvoiceListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[store_id]", filter.StoreId);
            Append(query, "filter[subscription_id]", filter.SubscriptionId);
            Append(query, "filter[user_email]", filter.UserEmail);
            Append(query, "filter[status]", filter.Status);
            if (filter.Refunded.HasValue)
            {
                Append(query, "filter[refunded]", filter.Refunded.Value ? "true" : "false");
            }
        }

        var requestUri = $"{ApiEndpoints.SubscriptionInvoices}{query}";
        var response = await GetJsonApiCollectionAsync<SubscriptionInvoice>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<SubscriptionInvoice>(
            response?.Data ?? Array.Empty<SubscriptionInvoice>(),
            response?.Links,
            response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<OrderInvoice>> GenerateInvoiceAsync(
        string id,
        GenerateSubscriptionInvoiceRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Subscription invoice id must be provided.", nameof(id));
        }

        var query = new StringBuilder();
        if (request is not null)
        {
            Append(query, "name", request.Name);
            Append(query, "address", request.Address);
            Append(query, "city", request.City);
            Append(query, "state", request.State);
            Append(query, "country", request.Country);
            Append(query, "zip_code", request.ZipCode);
            Append(query, "notes", request.Notes);
            Append(query, "locale", request.Locale);
        }

        var requestUri = $"{ApiEndpoints.SubscriptionInvoices}/{Uri.EscapeDataString(id)}/generate-invoice{query}";
        var response = await PostJsonApiAsync<OrderInvoice, object?>(requestUri, body: null, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<OrderInvoice>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<SubscriptionInvoice>> RefundAsync(
        string id,
        RefundSubscriptionInvoiceRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Subscription invoice id must be provided.", nameof(id));
        }

        var body = new JsonApiWriteRequest<RefundSubscriptionInvoiceAttributes>
        {
            Data = new JsonApiWriteData<RefundSubscriptionInvoiceAttributes>
            {
                Type = ResourceType,
                Id = id,
                Attributes = new RefundSubscriptionInvoiceAttributes
                {
                    Amount = request?.Amount,
                },
            },
        };

        var requestUri = $"{ApiEndpoints.SubscriptionInvoices}/{Uri.EscapeDataString(id)}/refund";
        var response = await PostJsonApiAsync<SubscriptionInvoice, JsonApiWriteRequest<RefundSubscriptionInvoiceAttributes>>(
                requestUri, body, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<SubscriptionInvoice>(response?.Data, response?.Links, response?.Meta);
    }
}
