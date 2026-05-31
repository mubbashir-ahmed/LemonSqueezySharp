using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IDiscountsClient"/>
public sealed class DiscountsClient : ApiClientBase, IDiscountsClient
{
    public DiscountsClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
