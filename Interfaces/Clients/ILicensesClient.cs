using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Licenses;

namespace LemonSqueezy.NET.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy License API endpoints (<c>/v1/licenses/*</c>).
/// These are distinct from the JSON:API <c>/v1/license-keys</c> resource —
/// they use form-encoded POSTs and return flat JSON.
/// See https://docs.lemonsqueezy.com/api/license-api.
/// </summary>
public interface ILicensesClient : IApiClient
{
    /// <summary>
    /// Activates a license key on a new instance (e.g. a customer's device)
    /// — <c>POST /v1/licenses/activate</c>.
    /// </summary>
    /// <param name="licenseKey">The license key string.</param>
    /// <param name="instanceName">Caller-supplied label for the activation (e.g. machine name).</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    Task<LicenseActivationResponse?> ActivateAsync(
        string licenseKey,
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deactivates an instance of a license key — <c>POST /v1/licenses/deactivate</c>.
    /// </summary>
    /// <param name="licenseKey">The license key string.</param>
    /// <param name="instanceId">The instance UUID returned by activation.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    Task<LicenseDeactivationResponse?> DeactivateAsync(
        string licenseKey,
        string instanceId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates a license key (and optionally its instance)
    /// — <c>POST /v1/licenses/validate</c>.
    /// </summary>
    /// <param name="licenseKey">The license key string.</param>
    /// <param name="instanceId">Optional instance UUID — when supplied, also verifies the instance is active.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    Task<LicenseValidationResponse?> ValidateAsync(
        string licenseKey,
        string? instanceId = null,
        CancellationToken cancellationToken = default);
}
