using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Models.UsageRecords;

public sealed class UsageRecord : JsonApiResource<UsageRecordAttributes>
{
}

/// <summary>
/// Usage-record attribute payload.
/// See https://docs.lemonsqueezy.com/api/usage-records.
/// </summary>
public sealed class UsageRecordAttributes
{
}
