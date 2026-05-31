using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.UsageRecords;

/// <summary>
/// Filter for <c>GET /v1/usage-records</c>.
/// </summary>
public sealed class UsageRecordListFilter : ListFilter
{
    public string? SubscriptionItemId { get; set; }
}
