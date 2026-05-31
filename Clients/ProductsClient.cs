using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IProductsClient"/>
public sealed class ProductsClient : ApiClientBase, IProductsClient
{
    public ProductsClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
