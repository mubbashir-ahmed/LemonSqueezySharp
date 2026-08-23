using LemonSqueezySharp.Interfaces.Clients.Base;
using LemonSqueezySharp.Models.Common;
using LemonSqueezySharp.Models.UsageRecords;

namespace LemonSqueezySharp.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/usage-records</c> endpoints.
/// See https://docs.lemonsqueezy.com/api/usage-records.
/// </summary>
public interface IUsageRecordsClient : IApiClient
{
    /// <summary>
    /// Records usage for a metered subscription item — <c>POST /v1/usage-records</c>.
    /// </summary>
    Task<BaseResult<UsageRecord>> CreateAsync(
        CreateUsageRecordRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a usage record by id — <c>GET /v1/usage-records/{id}</c>.
    /// </summary>
    Task<BaseResult<UsageRecord>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists usage records, optionally filtered by subscription item
    /// — <c>GET /v1/usage-records</c>.
    /// </summary>
    Task<PagedBaseResult<UsageRecord>> ListAsync(
        UsageRecordListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
