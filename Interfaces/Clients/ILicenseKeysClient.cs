using LemonSqueezySharp.Interfaces.Clients.Base;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.LicenseKeys;

namespace LemonSqueezySharp.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/license-keys</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/license-keys.
/// </summary>
public interface ILicenseKeysClient : IApiClient
{
    /// <summary>
    /// Retrieves a license key by id — <c>GET /v1/license-keys/{id}</c>.
    /// </summary>
    Task<BaseResult<LicenseKey>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a license key (activation limit, disabled flag, expires-at)
    /// — <c>PATCH /v1/license-keys/{id}</c>.
    /// </summary>
    Task<BaseResult<LicenseKey>> UpdateAsync(
        string id,
        UpdateLicenseKeyRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists license keys, optionally filtered by store / product / status
    /// — <c>GET /v1/license-keys</c>.
    /// </summary>
    Task<PagedBaseResult<LicenseKey>> ListAsync(
        LicenseKeyListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
