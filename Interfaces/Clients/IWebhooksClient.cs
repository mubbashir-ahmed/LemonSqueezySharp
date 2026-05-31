using LemonSqueezy.NET.Interfaces.Clients.Base;
using LemonSqueezy.NET.Models.Common;
using LemonSqueezy.NET.Models.Webhooks;

namespace LemonSqueezy.NET.Interfaces.Clients;

/// <summary>
/// Client for the LemonSqueezy <c>/v1/webhooks</c> endpoints (server-side webhook
/// subscription management).
/// See https://docs.lemonsqueezy.com/api/webhooks.
/// </summary>
public interface IWebhooksClient : IApiClient
{
    /// <summary>
    /// Creates a new webhook subscription — <c>POST /v1/webhooks</c>.
    /// </summary>
    Task<BaseResult<Webhook>> CreateAsync(
        CreateWebhookRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a webhook subscription by id — <c>GET /v1/webhooks/{id}</c>.
    /// </summary>
    Task<BaseResult<Webhook>> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a webhook subscription — <c>PATCH /v1/webhooks/{id}</c>.
    /// </summary>
    Task<BaseResult<Webhook>> UpdateAsync(
        string id,
        UpdateWebhookRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a webhook subscription — <c>DELETE /v1/webhooks/{id}</c>.
    /// Returns <c>204 No Content</c>.
    /// </summary>
    Task DeleteAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists webhook subscriptions, optionally filtered by store
    /// — <c>GET /v1/webhooks</c>.
    /// </summary>
    Task<PagedBaseResult<Webhook>> ListAsync(
        WebhookListFilter? filter = null,
        CancellationToken cancellationToken = default);
}
