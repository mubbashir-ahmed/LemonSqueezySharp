namespace LemonSqueezy.NET.Models.Subscriptions;

/// <summary>
/// Inputs for <c>PATCH /v1/subscriptions/{id}</c>. Only set the properties you
/// want to update — <see langword="null"/> values are dropped from the wire payload.
/// </summary>
public sealed class UpdateSubscriptionRequest
{
    /// <summary>
    /// Switch the subscription to a different variant (plan change).
    /// </summary>
    public long? VariantId { get; set; }

    /// <summary>
    /// Switch the subscription to a different product.
    /// </summary>
    public long? ProductId { get; set; }

    /// <summary>
    /// Day of month (1–31) the subscription should renew on.
    /// </summary>
    public int? BillingAnchor { get; set; }

    /// <summary>
    /// Bill any proration arising from a variant swap immediately.
    /// </summary>
    public bool? InvoiceImmediately { get; set; }

    /// <summary>
    /// Suppress proration when swapping variants.
    /// </summary>
    public bool? DisableProrations { get; set; }

    /// <summary>
    /// Reactivate a previously-cancelled subscription by setting to <c>false</c>.
    /// </summary>
    public bool? Cancelled { get; set; }

    /// <summary>
    /// Override the trial end date.
    /// </summary>
    public DateTimeOffset? TrialEndsAt { get; set; }

    /// <summary>
    /// Pause the subscription. Set to <see langword="null"/> to unpause.
    /// </summary>
    public SubscriptionPauseRequest? Pause { get; set; }
}

/// <summary>
/// Inputs for the <c>pause</c> attribute of a subscription update.
/// </summary>
public sealed class SubscriptionPauseRequest
{
    /// <summary>One of <c>void</c> or <c>free</c>.</summary>
    public string? Mode { get; set; }

    /// <summary>When the pause should resume.</summary>
    public DateTimeOffset? ResumesAt { get; set; }
}
