using LemonSqueezySharp.Models.Common;

namespace LemonSqueezySharp.Models.Customers;

/// <summary>
/// Strongly-typed alias for a LemonSqueezy customer resource.
/// </summary>
public sealed class Customer : JsonApiResource<CustomerAttributes>
{
}
