using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Orders;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IOrdersClient"/>
public sealed class OrdersClient : ApiClientBase, IOrdersClient
{
    private const string ResourceType = "orders";

    public OrdersClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<Order>> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Order id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Orders}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<Order>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Order>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<Order>> ListAsync(
        OrderListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[store_id]", filter.StoreId);
            Append(query, "filter[user_email]", filter.UserEmail);
        }

        var requestUri = $"{ApiEndpoints.Orders}{query}";
        var response = await GetJsonApiCollectionAsync<Order>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<Order>(
            response?.Data ?? Array.Empty<Order>(),
            response?.Links,
            response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<OrderInvoice>> GenerateInvoiceAsync(
        string id,
        GenerateOrderInvoiceRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Order id must be provided.", nameof(id));
        }

        var query = new System.Text.StringBuilder();
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

        var requestUri = $"{ApiEndpoints.Orders}/{Uri.EscapeDataString(id)}/generate-invoice{query}";
        var response = await PostJsonApiAsync<OrderInvoice, object?>(requestUri, body: null, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<OrderInvoice>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<BaseResult<Order>> RefundAsync(
        string id,
        RefundOrderRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Order id must be provided.", nameof(id));
        }

        var body = new JsonApiWriteRequest<RefundOrderAttributes>
        {
            Data = new JsonApiWriteData<RefundOrderAttributes>
            {
                Type = ResourceType,
                Id = id,
                Attributes = new RefundOrderAttributes
                {
                    Amount = request?.Amount,
                },
            },
        };

        var requestUri = $"{ApiEndpoints.Orders}/{Uri.EscapeDataString(id)}/refund";
        var response = await PostJsonApiAsync<Order, JsonApiWriteRequest<RefundOrderAttributes>>(requestUri, body, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<Order>(response?.Data, response?.Links, response?.Meta);
    }
}
