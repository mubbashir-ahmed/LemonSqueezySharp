namespace LemonSqueezy.NET.Models.SubscriptionItems;

/// <summary>
/// Inputs for <c>PATCH /v1/subscription-items/{id}</c>.
/// </summary>
public sealed class UpdateSubscriptionItemRequest
{
    /// <summary>
    /// New quantity for the subscription item.
    /// </summary>
    public int? Quantity { get; set; }

    /// <summary>
    /// Bill any proration arising from the quantity change immediately.
    /// </summary>
    public bool? InvoiceImmediately { get; set; }

    /// <summary>
    /// Suppress proration when changing quantity.
    /// </summary>
    public bool? DisableProrations { get; set; }
}
