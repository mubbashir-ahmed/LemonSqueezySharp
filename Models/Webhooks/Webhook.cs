using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Models.Webhooks;

/// <summary>
/// Strongly-typed alias for a LemonSqueezy webhook resource.
/// </summary>
public sealed class Webhook : JsonApiResource<WebhookAttributes>
{
}
