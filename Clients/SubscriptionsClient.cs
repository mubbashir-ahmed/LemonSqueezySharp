using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Clients.Interfaces;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ISubscriptionsClient"/>
public sealed class SubscriptionsClient : ApiClientBase, ISubscriptionsClient
{
    public SubscriptionsClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
