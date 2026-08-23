using LemonSqueezySharp.Models.Requests;

namespace LemonSqueezySharp.Models.Webhooks;

/// <summary>
/// Filter for <c>GET /v1/webhooks</c>.
/// </summary>
public sealed class WebhookListFilter : ListFilter
{
    /// <summary>Only return webhooks belonging to the store with this identifier.</summary>
    public string? StoreId { get; set; }
}
