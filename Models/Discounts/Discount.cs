using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Models.Discounts;

public sealed class Discount : JsonApiResource<DiscountAttributes>
{
}

/// <summary>
/// Discount attribute payload. See https://docs.lemonsqueezy.com/api/discounts.
/// </summary>
public sealed class DiscountAttributes
{
}
