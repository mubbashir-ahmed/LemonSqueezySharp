namespace LemonSqueezy.NET.Models.Webhooks.Events;

/// <summary>
/// The set of webhook <c>event_name</c> values emitted by LemonSqueezy. Use these constants
/// instead of magic strings when configuring webhooks or comparing <see cref="WebhookMeta.EventName"/>.
/// </summary>
public static class WebhookEventNames
{
    public const string OrderCreated = "order_created";
    public const string OrderRefunded = "order_refunded";

    public const string CustomerUpdated = "customer_updated";

    public const string DisputeCreated = "dispute_created";
    public const string DisputeResolved = "dispute_resolved";

    public const string SubscriptionCreated = "subscription_created";
    public const string SubscriptionUpdated = "subscription_updated";
    public const string SubscriptionCancelled = "subscription_cancelled";
    public const string SubscriptionResumed = "subscription_resumed";
    public const string SubscriptionExpired = "subscription_expired";
    public const string SubscriptionPaused = "subscription_paused";
    public const string SubscriptionUnpaused = "subscription_unpaused";
    public const string SubscriptionPlanChanged = "subscription_plan_changed";

    public const string SubscriptionPaymentSuccess = "subscription_payment_success";
    public const string SubscriptionPaymentFailed = "subscription_payment_failed";
    public const string SubscriptionPaymentRecovered = "subscription_payment_recovered";
    public const string SubscriptionPaymentRefunded = "subscription_payment_refunded";

    public const string LicenseKeyCreated = "license_key_created";
    public const string LicenseKeyUpdated = "license_key_updated";

    public const string AffiliateActivated = "affiliate_activated";
}
