using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.LicenseKeyInstances;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ILicenseKeyInstancesClient"/>
public sealed class LicenseKeyInstancesClient : ApiClientBase, ILicenseKeyInstancesClient
{
    /// <summary>Initializes a new instance of the <see cref="LicenseKeyInstancesClient"/> class.</summary>
    public LicenseKeyInstancesClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<BaseResult<LicenseKeyInstance>> GetAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("License key instance id must be provided.", nameof(id));
        }

        var requestUri = $"{ApiEndpoints.LicenseKeyInstances}/{Uri.EscapeDataString(id)}";
        var response = await GetJsonApiAsync<LicenseKeyInstance>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new BaseResult<LicenseKeyInstance>(response?.Data, response?.Links, response?.Meta);
    }

    /// <inheritdoc />
    public async Task<PagedBaseResult<LicenseKeyInstance>> ListAsync(
        LicenseKeyInstanceListFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildListQueryBuilder(filter);
        if (filter is not null)
        {
            Append(query, "filter[license_key_id]", filter.LicenseKeyId);
        }

        var requestUri = $"{ApiEndpoints.LicenseKeyInstances}{query}";
        var response = await GetJsonApiCollectionAsync<LicenseKeyInstance>(requestUri, cancellationToken)
            .ConfigureAwait(false);

        return new PagedBaseResult<LicenseKeyInstance>(
            response?.Data ?? Array.Empty<LicenseKeyInstance>(),
            response?.Links,
            response?.Meta);
    }
}
