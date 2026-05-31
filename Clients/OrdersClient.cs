using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Clients.Interfaces;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IOrdersClient"/>
public sealed class OrdersClient : ApiClientBase, IOrdersClient
{
    public OrdersClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
