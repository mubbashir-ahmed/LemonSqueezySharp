using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IDiscountRedemptionsClient"/>
public sealed class DiscountRedemptionsClient : ApiClientBase, IDiscountRedemptionsClient
{
    public DiscountRedemptionsClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
