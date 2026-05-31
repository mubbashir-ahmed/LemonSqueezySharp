using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Clients.Interfaces;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ICheckoutsClient"/>
public sealed class CheckoutsClient : ApiClientBase, ICheckoutsClient
{
    public CheckoutsClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
