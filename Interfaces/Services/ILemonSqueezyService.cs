using LemonSqueezy.NET.Interfaces.Clients;

namespace LemonSqueezy.NET.Interfaces.Services;

/// <summary>
/// Optional high-level façade for consumers who prefer a single entry point with
/// added cross-resource helpers (webhook handling, etc.). Wraps
/// <see cref="ILemonSqueezyClient"/> and exposes additional services.
/// </summary>
public interface ILemonSqueezyService
{
    /// <summary>
    /// The root API client exposing every resource client (e.g. <c>Client.Subscriptions</c>,
    /// <c>Client.Orders</c>) for making calls to the LemonSqueezy API.
    /// </summary>
    ILemonSqueezyClient Client { get; }

    /// <summary>
    /// Verifies the HMAC-SHA256 signature of inbound webhook payloads against the
    /// <c>X-Signature</c> header. Use this when you only need authentication and will
    /// parse the body yourself.
    /// </summary>
    IWebhookVerifier WebhookVerifier { get; }

    /// <summary>
    /// Verifies and parses inbound webhooks into the strongly-typed event hierarchy,
    /// letting you pattern-match each event (e.g. <c>OrderCreatedEvent</c>) instead of
    /// inspecting raw JSON.
    /// </summary>
    IWebhookEventParser WebhookEvents { get; }
}
