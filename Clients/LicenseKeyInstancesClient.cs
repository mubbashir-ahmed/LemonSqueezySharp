using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ILicenseKeyInstancesClient"/>
public sealed class LicenseKeyInstancesClient : ApiClientBase, ILicenseKeyInstancesClient
{
    public LicenseKeyInstancesClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
