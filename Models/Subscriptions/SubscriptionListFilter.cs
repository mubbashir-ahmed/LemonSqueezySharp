using LemonSqueezy.NET.Models.Requests;

namespace LemonSqueezy.NET.Models.Subscriptions;

/// <summary>
/// Filter for <c>GET /v1/subscriptions</c>.
/// </summary>
public sealed class SubscriptionListFilter : ListFilter
{
    /// <summary>
    /// Filter subscriptions by store id (<c>filter[store_id]</c>).
    /// </summary>
    public string? StoreId { get; set; }

    /// <summary>
    /// Filter subscriptions by order id (<c>filter[order_id]</c>).
    /// </summary>
    public string? OrderId { get; set; }

    /// <summary>
    /// Filter subscriptions by order item id (<c>filter[order_item_id]</c>).
    /// </summary>
    public string? OrderItemId { get; set; }

    /// <summary>
    /// Filter subscriptions by product id (<c>filter[product_id]</c>).
    /// </summary>
    public string? ProductId { get; set; }

    /// <summary>
    /// Filter subscriptions by variant id (<c>filter[variant_id]</c>).
    /// </summary>
    public string? VariantId { get; set; }

    /// <summary>
    /// Filter subscriptions by customer email address (<c>filter[user_email]</c>).
    /// </summary>
    public string? UserEmail { get; set; }

    /// <summary>
    /// Filter subscriptions by status (<c>filter[status]</c>) — e.g. <c>"active"</c>, <c>"cancelled"</c>.
    /// </summary>
    public string? Status { get; set; }
}
