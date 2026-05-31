using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.Webhooks;

/// <summary>
/// Filter for <c>GET /v1/webhooks</c>.
/// </summary>
public sealed class WebhookListFilter : ListFilter
{
    public string? StoreId { get; set; }
}
