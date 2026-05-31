using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ICheckoutsClient"/>
public sealed class CheckoutsClient : ApiClientBase, ICheckoutsClient
{
    public CheckoutsClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
