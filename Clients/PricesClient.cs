using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IPricesClient"/>
public sealed class PricesClient : ApiClientBase, IPricesClient
{
    public PricesClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
