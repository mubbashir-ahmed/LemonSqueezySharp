using LemonSqueezy.NET.Models.Affiliates;
using LemonSqueezy.NET.Models.Customers;
using LemonSqueezy.NET.Models.Disputes;
using LemonSqueezy.NET.Models.LicenseKeys;
using LemonSqueezy.NET.Models.Orders;
using LemonSqueezy.NET.Models.SubscriptionInvoices;
using LemonSqueezy.NET.Models.Subscriptions;

namespace LemonSqueezy.NET.Models.Webhooks.Events;

// One sealed record per LemonSqueezy event name. The closed set lets consumers pattern-match
// exhaustively over LemonSqueezyWebhookEvent. Each carries its typed resource in Data.

// --- Orders ---------------------------------------------------------------------------------

/// <summary>Raised when a new order is created (<c>order_created</c>).</summary>
public sealed record OrderCreatedEvent : LemonSqueezyWebhookEvent<Order>;

/// <summary>Raised when an order is refunded (<c>order_refunded</c>).</summary>
public sealed record OrderRefundedEvent : LemonSqueezyWebhookEvent<Order>;

// --- Customers ------------------------------------------------------------------------------

/// <summary>Raised when a customer is updated (<c>customer_updated</c>).</summary>
public sealed record CustomerUpdatedEvent : LemonSqueezyWebhookEvent<Customer>;

// --- Disputes -------------------------------------------------------------------------------

/// <summary>Raised when a payment dispute is opened (<c>dispute_created</c>).</summary>
public sealed record DisputeCreatedEvent : LemonSqueezyWebhookEvent<Dispute>;

/// <summary>Raised when a payment dispute is resolved (<c>dispute_resolved</c>).</summary>
public sealed record DisputeResolvedEvent : LemonSqueezyWebhookEvent<Dispute>;

// --- Subscription lifecycle -----------------------------------------------------------------

/// <summary>Raised when a subscription is created (<c>subscription_created</c>).</summary>
public sealed record SubscriptionCreatedEvent : LemonSqueezyWebhookEvent<Subscription>;

/// <summary>Raised when a subscription is updated (<c>subscription_updated</c>).</summary>
public sealed record SubscriptionUpdatedEvent : LemonSqueezyWebhookEvent<Subscription>;

/// <summary>Raised when a subscription is cancelled (<c>subscription_cancelled</c>).</summary>
public sealed record SubscriptionCancelledEvent : LemonSqueezyWebhookEvent<Subscription>;

/// <summary>Raised when a cancelled subscription is resumed (<c>subscription_resumed</c>).</summary>
public sealed record SubscriptionResumedEvent : LemonSqueezyWebhookEvent<Subscription>;

/// <summary>Raised when a subscription expires (<c>subscription_expired</c>).</summary>
public sealed record SubscriptionExpiredEvent : LemonSqueezyWebhookEvent<Subscription>;

/// <summary>Raised when a subscription is paused (<c>subscription_paused</c>).</summary>
public sealed record SubscriptionPausedEvent : LemonSqueezyWebhookEvent<Subscription>;

/// <summary>Raised when a paused subscription resumes billing (<c>subscription_unpaused</c>).</summary>
public sealed record SubscriptionUnpausedEvent : LemonSqueezyWebhookEvent<Subscription>;

/// <summary>Raised when a subscription's plan changes (<c>subscription_plan_changed</c>).</summary>
public sealed record SubscriptionPlanChangedEvent : LemonSqueezyWebhookEvent<Subscription>;

// --- Subscription payments (carry a subscription invoice) ------------------------------------

/// <summary>Raised when a subscription payment succeeds (<c>subscription_payment_success</c>).</summary>
public sealed record SubscriptionPaymentSuccessEvent : LemonSqueezyWebhookEvent<SubscriptionInvoice>;

/// <summary>Raised when a subscription payment fails (<c>subscription_payment_failed</c>).</summary>
public sealed record SubscriptionPaymentFailedEvent : LemonSqueezyWebhookEvent<SubscriptionInvoice>;

/// <summary>Raised when a failed subscription payment is recovered (<c>subscription_payment_recovered</c>).</summary>
public sealed record SubscriptionPaymentRecoveredEvent : LemonSqueezyWebhookEvent<SubscriptionInvoice>;

/// <summary>Raised when a subscription payment is refunded (<c>subscription_payment_refunded</c>).</summary>
public sealed record SubscriptionPaymentRefundedEvent : LemonSqueezyWebhookEvent<SubscriptionInvoice>;

// --- License keys ---------------------------------------------------------------------------

/// <summary>Raised when a license key is created (<c>license_key_created</c>).</summary>
public sealed record LicenseKeyCreatedEvent : LemonSqueezyWebhookEvent<LicenseKey>;

/// <summary>Raised when a license key is updated (<c>license_key_updated</c>).</summary>
public sealed record LicenseKeyUpdatedEvent : LemonSqueezyWebhookEvent<LicenseKey>;

// --- Affiliates -----------------------------------------------------------------------------

/// <summary>Raised when an affiliate is activated (<c>affiliate_activated</c>).</summary>
public sealed record AffiliateActivatedEvent : LemonSqueezyWebhookEvent<Affiliate>;
