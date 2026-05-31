using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Http;
using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Models.Licenses;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ILicensesClient"/>
public sealed class LicensesClient : ApiClientBase, ILicensesClient
{
    public LicensesClient(HttpClient httpClient) : base(httpClient)
    {
    }

    /// <inheritdoc />
    public async Task<LicenseActivationResponse?> ActivateAsync(
        string licenseKey,
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(licenseKey))
        {
            throw new ArgumentException("License key must be provided.", nameof(licenseKey));
        }

        if (string.IsNullOrWhiteSpace(instanceName))
        {
            throw new ArgumentException("Instance name must be provided.", nameof(instanceName));
        }

        var form = new[]
        {
            new KeyValuePair<string, string>("license_key", licenseKey),
            new KeyValuePair<string, string>("instance_name", instanceName),
        };

        var requestUri = $"{ApiEndpoints.Licenses}/activate";
        return await PostFormAsync<LicenseActivationResponse>(requestUri, form, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<LicenseDeactivationResponse?> DeactivateAsync(
        string licenseKey,
        string instanceId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(licenseKey))
        {
            throw new ArgumentException("License key must be provided.", nameof(licenseKey));
        }

        if (string.IsNullOrWhiteSpace(instanceId))
        {
            throw new ArgumentException("Instance id must be provided.", nameof(instanceId));
        }

        var form = new[]
        {
            new KeyValuePair<string, string>("license_key", licenseKey),
            new KeyValuePair<string, string>("instance_id", instanceId),
        };

        var requestUri = $"{ApiEndpoints.Licenses}/deactivate";
        return await PostFormAsync<LicenseDeactivationResponse>(requestUri, form, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<LicenseValidationResponse?> ValidateAsync(
        string licenseKey,
        string? instanceId = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(licenseKey))
        {
            throw new ArgumentException("License key must be provided.", nameof(licenseKey));
        }

        var form = new List<KeyValuePair<string, string>>
        {
            new("license_key", licenseKey),
        };

        if (!string.IsNullOrWhiteSpace(instanceId) && instanceId != null)
        {
            form.Add(new KeyValuePair<string, string>("instance_id", instanceId));
        }

        var requestUri = $"{ApiEndpoints.Licenses}/validate";
        return await PostFormAsync<LicenseValidationResponse>(requestUri, form, cancellationToken)
            .ConfigureAwait(false);
    }
}
