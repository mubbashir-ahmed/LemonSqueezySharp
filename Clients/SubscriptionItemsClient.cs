using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ISubscriptionItemsClient"/>
public sealed class SubscriptionItemsClient : ApiClientBase, ISubscriptionItemsClient
{
    public SubscriptionItemsClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
