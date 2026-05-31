using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.Subscriptions;

/// <summary>
/// Filter for <c>GET /v1/subscriptions</c>.
/// </summary>
public sealed class SubscriptionListFilter : ListFilter
{
    public string? StoreId { get; set; }
    public string? OrderId { get; set; }
    public string? OrderItemId { get; set; }
    public string? ProductId { get; set; }
    public string? VariantId { get; set; }
    public string? UserEmail { get; set; }

    /// <summary>
    /// Filter subscriptions by status (<c>filter[status]</c>) — e.g. <c>"active"</c>, <c>"cancelled"</c>.
    /// </summary>
    public string? Status { get; set; }
}
