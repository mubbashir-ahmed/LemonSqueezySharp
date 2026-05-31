using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Models.Checkouts;

public sealed class Checkout : JsonApiResource<CheckoutAttributes>
{
}

/// <summary>
/// Checkout attribute payload. See https://docs.lemonsqueezy.com/api/checkouts.
/// </summary>
public sealed class CheckoutAttributes
{
}
