using LemonSqueezy.NET.Interfaces.Clients;
using LemonSqueezy.NET.Interfaces.Services;

namespace LemonSqueezy.NET.Services;

/// <inheritdoc cref="ILemonSqueezyService"/>
public sealed class LemonSqueezyService : ILemonSqueezyService
{
    public LemonSqueezyService(ILemonSqueezyClient client, IWebhookVerifier webhookVerifier)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
        WebhookVerifier = webhookVerifier ?? throw new ArgumentNullException(nameof(webhookVerifier));
    }

    public ILemonSqueezyClient Client { get; }

    public IWebhookVerifier WebhookVerifier { get; }
}
