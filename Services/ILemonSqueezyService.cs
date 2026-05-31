using LemonSqueezy.NET.Clients.Interfaces;

namespace LemonSqueezy.NET.Services;

/// <summary>
/// Optional high-level façade for consumers who prefer a single entry point with
/// added cross-resource helpers (webhook handling, etc.). Wraps
/// <see cref="ILemonSqueezyClient"/> and exposes additional services.
/// </summary>
public interface ILemonSqueezyService
{
    ILemonSqueezyClient Client { get; }

    IWebhookVerifier WebhookVerifier { get; }
}
