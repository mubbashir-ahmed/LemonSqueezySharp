namespace LemonSqueezy.NET.Models.Webhooks.Events;

/// <summary>
/// The set of webhook <c>event_name</c> values emitted by LemonSqueezy. Use these constants
/// instead of magic strings when configuring webhooks or comparing <see cref="WebhookMeta.EventName"/>.
/// </summary>
public static class WebhookEventNames
{
    /// <summary>Event name <c>order_created</c>: a new order was created.</summary>
    public const string OrderCreated = "order_created";
    /// <summary>Event name <c>order_refunded</c>: an order was refunded.</summary>
    public const string OrderRefunded = "order_refunded";

    /// <summary>Event name <c>customer_updated</c>: a customer record was updated.</summary>
    public const string CustomerUpdated = "customer_updated";

    /// <summary>Event name <c>dispute_created</c>: a payment dispute was opened.</summary>
    public const string DisputeCreated = "dispute_created";
    /// <summary>Event name <c>dispute_resolved</c>: a payment dispute was resolved.</summary>
    public const string DisputeResolved = "dispute_resolved";

    /// <summary>Event name <c>subscription_created</c>: a subscription was created.</summary>
    public const string SubscriptionCreated = "subscription_created";
    /// <summary>Event name <c>subscription_updated</c>: a subscription was updated.</summary>
    public const string SubscriptionUpdated = "subscription_updated";
    /// <summary>Event name <c>subscription_cancelled</c>: a subscription was cancelled.</summary>
    public const string SubscriptionCancelled = "subscription_cancelled";
    /// <summary>Event name <c>subscription_resumed</c>: a cancelled subscription was resumed.</summary>
    public const string SubscriptionResumed = "subscription_resumed";
    /// <summary>Event name <c>subscription_expired</c>: a subscription expired.</summary>
    public const string SubscriptionExpired = "subscription_expired";
    /// <summary>Event name <c>subscription_paused</c>: a subscription was paused.</summary>
    public const string SubscriptionPaused = "subscription_paused";
    /// <summary>Event name <c>subscription_unpaused</c>: a paused subscription resumed billing.</summary>
    public const string SubscriptionUnpaused = "subscription_unpaused";
    /// <summary>Event name <c>subscription_plan_changed</c>: a subscription's plan changed.</summary>
    public const string SubscriptionPlanChanged = "subscription_plan_changed";

    /// <summary>Event name <c>subscription_payment_success</c>: a subscription payment succeeded.</summary>
    public const string SubscriptionPaymentSuccess = "subscription_payment_success";
    /// <summary>Event name <c>subscription_payment_failed</c>: a subscription payment failed.</summary>
    public const string SubscriptionPaymentFailed = "subscription_payment_failed";
    /// <summary>Event name <c>subscription_payment_recovered</c>: a failed subscription payment was recovered.</summary>
    public const string SubscriptionPaymentRecovered = "subscription_payment_recovered";
    /// <summary>Event name <c>subscription_payment_refunded</c>: a subscription payment was refunded.</summary>
    public const string SubscriptionPaymentRefunded = "subscription_payment_refunded";

    /// <summary>Event name <c>license_key_created</c>: a license key was created.</summary>
    public const string LicenseKeyCreated = "license_key_created";
    /// <summary>Event name <c>license_key_updated</c>: a license key was updated.</summary>
    public const string LicenseKeyUpdated = "license_key_updated";

    /// <summary>Event name <c>affiliate_activated</c>: an affiliate was activated.</summary>
    public const string AffiliateActivated = "affiliate_activated";
}
