using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ISubscriptionsClient"/>
public sealed class SubscriptionsClient : ApiClientBase, ISubscriptionsClient
{
    public SubscriptionsClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
