using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Clients.Interfaces;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="ILicenseKeysClient"/>
public sealed class LicenseKeysClient : ApiClientBase, ILicenseKeysClient
{
    public LicenseKeysClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
