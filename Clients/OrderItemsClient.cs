using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IOrderItemsClient"/>
public sealed class OrderItemsClient : ApiClientBase, IOrderItemsClient
{
    public OrderItemsClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
