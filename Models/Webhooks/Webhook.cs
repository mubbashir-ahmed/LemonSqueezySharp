using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Models.Webhooks;

public sealed class Webhook : JsonApiResource<WebhookAttributes>
{
}

/// <summary>
/// Webhook-subscription attribute payload (registering callback URLs with LemonSqueezy).
/// See https://docs.lemonsqueezy.com/api/webhooks.
/// </summary>
public sealed class WebhookAttributes
{
}
