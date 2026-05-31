using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Clients.Interfaces;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IWebhooksClient"/>
public sealed class WebhooksClient : ApiClientBase, IWebhooksClient
{
    public WebhooksClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
