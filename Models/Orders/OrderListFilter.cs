using LemonSqueezySharp.Models.Requests;

namespace LemonSqueezySharp.Models.Orders;

/// <summary>
/// Filter for <c>GET /v1/orders</c>.
/// </summary>
public sealed class OrderListFilter : ListFilter
{
    /// <summary>
    /// Filter orders by store id (<c>filter[store_id]</c>).
    /// </summary>
    public string? StoreId { get; set; }

    /// <summary>
    /// Filter orders by customer email address (<c>filter[user_email]</c>).
    /// </summary>
    public string? UserEmail { get; set; }
}
