using LemonSqueezySharp.Models.Requests;

namespace LemonSqueezySharp.Models.OrderItems;

/// <summary>
/// Filter for <c>GET /v1/order-items</c>.
/// </summary>
public sealed class OrderItemListFilter : ListFilter
{
    /// <summary>
    /// Filter order items by order id (<c>filter[order_id]</c>).
    /// </summary>
    public string? OrderId { get; set; }

    /// <summary>
    /// Filter order items by product id (<c>filter[product_id]</c>).
    /// </summary>
    public string? ProductId { get; set; }

    /// <summary>
    /// Filter order items by variant id (<c>filter[variant_id]</c>).
    /// </summary>
    public string? VariantId { get; set; }
}
