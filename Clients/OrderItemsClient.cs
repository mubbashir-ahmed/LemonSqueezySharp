using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.OrderItems;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IOrderItemsClient"/>
public sealed class OrderItemsClient : ApiClientBase, IOrderItemsClient
{
    /// <summary>Initializes a new instance of the <see cref="OrderItemsClient"/> class.</summary>
    public OrderItemsClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<OrderItem>> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Order item id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.OrderItems}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<OrderItem>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<OrderItem>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<OrderItem>> ListAsync(
        OrderItemListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[order_id]", filter.OrderId);
            Append(query, "filter[product_id]", filter.ProductId);
            Append(query, "filter[variant_id]", filter.VariantId);
        }

        var requestUri = $"{ApiEndpoints.OrderItems}{query}";
        var response = await GetJsonApiCollectionAsync<OrderItem>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<OrderItem>(
            response?.Data ?? Array.Empty<OrderItem>(),
            response?.Links,
            response?.Meta);
    }
}
