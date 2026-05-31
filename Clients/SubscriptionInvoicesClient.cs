using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Clients.Interfaces;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ISubscriptionInvoicesClient"/>
public sealed class SubscriptionInvoicesClient : ApiClientBase, ISubscriptionInvoicesClient
{
    public SubscriptionInvoicesClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
