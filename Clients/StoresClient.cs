using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Requests;
using LemonSqueezy.NET.Models.Stores;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IStoresClient"/>
public sealed class StoresClient : ApiClientBase, IStoresClient
{
    public StoresClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<Store>> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Store id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.Stores}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<Store>(requestUri, cancellationToken).ConfigureAwait(false);

        return new BaseResult<Store>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<Store>> ListAsync(
        ListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var requestUri = ApiEndpoints.Stores + BuildListQuery(filter);
        var response = await GetJsonApiCollectionAsync<Store>(requestUri, cancellationToken).ConfigureAwait(false);

        return new PagedBaseResult<Store>(
            response?.Data ?? Array.Empty<Store>(),
            response?.Links,
            response?.Meta);
    }
}
