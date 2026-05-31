using LemonSqueezy.NET.Clients.Base;
using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Clients;

/// <inheritdoc cref="IUsageRecordsClient"/>
public sealed class UsageRecordsClient : ApiClientBase, IUsageRecordsClient
{
    public UsageRecordsClient(HttpClient httpClient) : base(httpClient)
    {
    }
}
