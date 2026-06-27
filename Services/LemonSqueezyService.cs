using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Interfaces.Services;

namespace LemonSqueezy.NET.Services;

/// <inheritdoc cref="ILemonSqueezyService"/>
public sealed class LemonSqueezyService : ILemonSqueezyService
{
    public LemonSqueezyService(
        ILemonSqueezyClient client,
        IWebhookVerifier webhookVerifier,
        IWebhookEventParser webhookEvents)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
        WebhookVerifier = webhookVerifier ?? throw new ArgumentNullException(nameof(webhookVerifier));
        WebhookEvents = webhookEvents ?? throw new ArgumentNullException(nameof(webhookEvents));
    }

    /// <inheritdoc />
    public ILemonSqueezyClient Client { get; }

    /// <inheritdoc />
    public IWebhookVerifier WebhookVerifier { get; }

    /// <inheritdoc />
    public IWebhookEventParser WebhookEvents { get; }
}
