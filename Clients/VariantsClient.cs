using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IVariantsClient"/>
public sealed class VariantsClient : ApiClientBase, IVariantsClient
{
    public VariantsClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
