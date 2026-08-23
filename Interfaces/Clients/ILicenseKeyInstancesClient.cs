using LemonSqueezySharp.Interfaces.Clients.Base;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.LicenseKeyInstances;

namespace LemonSqueezySharp.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/license-key-instances</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/license-key-instances.
/// </summary>
public interface ILicenseKeyInstancesClient : IApiClient
{
    /// <summary>
    /// Retrieves a license-key instance by id
    /// — <c>GET /v1/license-key-instances/{id}</c>.
    /// </summary>
    Task<BaseResult<LicenseKeyInstance>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists license-key instances, optionally filtered by license key
    /// — <c>GET /v1/license-key-instances</c>.
    /// </summary>
    Task<PagedBaseResult<LicenseKeyInstance>> ListAsync(
        LicenseKeyInstanceListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
