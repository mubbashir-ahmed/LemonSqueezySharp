using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Clients.Interfaces;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IUsageRecordsClient"/>
public sealed class UsageRecordsClient : ApiClientBase, IUsageRecordsClient
{
    public UsageRecordsClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
