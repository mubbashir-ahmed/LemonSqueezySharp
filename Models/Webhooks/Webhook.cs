using LemonSqueezySharp.Models.Common;

namespace LemonSqueezySharp.Models.Webhooks;

/// <summary>
/// Strongly-typed alias for a LemonSqueezy webhook resource.
/// </summary>
public sealed class Webhook : JsonApiResource<WebhookAttributes>
{
}
