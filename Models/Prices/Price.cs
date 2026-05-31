using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Models.Prices;

public sealed class Price : JsonApiResource<PriceAttributes>
{
}

/// <summary>
/// Price attribute payload. See https://docs.lemonsqueezy.com/api/prices.
/// </summary>
public sealed class PriceAttributes
{
}
