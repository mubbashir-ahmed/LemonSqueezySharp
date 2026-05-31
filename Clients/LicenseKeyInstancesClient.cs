using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Clients.Interfaces;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ILicenseKeyInstancesClient"/>
public sealed class LicenseKeyInstancesClient : ApiClientBase, ILicenseKeyInstancesClient
{
    public LicenseKeyInstancesClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
