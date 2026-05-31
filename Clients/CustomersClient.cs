using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ICustomersClient"/>
public sealed class CustomersClient : ApiClientBase, ICustomersClient
{
    public CustomersClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
