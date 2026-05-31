using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Clients.Interfaces;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IStoresClient"/>
public sealed class StoresClient : ApiClientBase, IStoresClient
{
    public StoresClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
