using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Subscriptions;

/// <summary>
/// Subscription attribute payload.
/// See https://docs.lemonsqueezy.com/api/subscriptions.
/// </summary>
public sealed class SubscriptionAttributes
{
    /// <summary>Id of the store this subscription belongs to (<c>store_id</c>).</summary>
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    /// <summary>Id of the customer who owns the subscription (<c>customer_id</c>).</summary>
    [JsonPropertyName("customer_id")]
    public long? CustomerId { get; set; }

    /// <summary>Id of the order that created the subscription (<c>order_id</c>).</summary>
    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    /// <summary>Id of the order item that created the subscription (<c>order_item_id</c>).</summary>
    [JsonPropertyName("order_item_id")]
    public long? OrderItemId { get; set; }

    /// <summary>Id of the subscribed product (<c>product_id</c>).</summary>
    [JsonPropertyName("product_id")]
    public long? ProductId { get; set; }

    /// <summary>Id of the subscribed variant (<c>variant_id</c>).</summary>
    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    /// <summary>Name of the subscribed product (<c>product_name</c>).</summary>
    [JsonPropertyName("product_name")]
    public string? ProductName { get; set; }

    /// <summary>Name of the subscribed variant (<c>variant_name</c>).</summary>
    [JsonPropertyName("variant_name")]
    public string? VariantName { get; set; }

    /// <summary>Name of the subscriber (<c>user_name</c>).</summary>
    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    /// <summary>Email of the subscriber (<c>user_email</c>).</summary>
    [JsonPropertyName("user_email")]
    public string? UserEmail { get; set; }

    /// <summary>Lifecycle status of the subscription (<c>status</c>).</summary>
    [JsonPropertyName("status")]
    public SubscriptionStatus? Status { get; set; }

    /// <summary>Human-readable formatted status of the subscription (<c>status_formatted</c>).</summary>
    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    /// <summary>Brand of the card used for billing (<c>card_brand</c>).</summary>
    [JsonPropertyName("card_brand")]
    public string? CardBrand { get; set; }

    /// <summary>Last four digits of the card used for billing (<c>card_last_four</c>).</summary>
    [JsonPropertyName("card_last_four")]
    public string? CardLastFour { get; set; }

    /// <summary>Pause-mode metadata for the subscription (<c>pause</c>).</summary>
    [JsonPropertyName("pause")]
    public SubscriptionPause? Pause { get; set; }

    /// <summary>Whether the subscription has been cancelled (<c>cancelled</c>).</summary>
    [JsonPropertyName("cancelled")]
    public bool? Cancelled { get; set; }

    /// <summary>Date and time the trial ends (<c>trial_ends_at</c>).</summary>
    [JsonPropertyName("trial_ends_at")]
    public DateTimeOffset? TrialEndsAt { get; set; }

    /// <summary>Day of month (1–31) the subscription renews on (<c>billing_anchor</c>).</summary>
    [JsonPropertyName("billing_anchor")]
    public int? BillingAnchor { get; set; }

    /// <summary>Summary of the first subscription item (<c>first_subscription_item</c>).</summary>
    [JsonPropertyName("first_subscription_item")]
    public SubscriptionFirstItem? FirstSubscriptionItem { get; set; }

    /// <summary>Self-service URLs exposed on the subscription (<c>urls</c>).</summary>
    [JsonPropertyName("urls")]
    public SubscriptionUrls? Urls { get; set; }

    /// <summary>Date and time the subscription next renews (<c>renews_at</c>).</summary>
    [JsonPropertyName("renews_at")]
    public DateTimeOffset? RenewsAt { get; set; }

    /// <summary>Date and time the subscription ends, if cancelled (<c>ends_at</c>).</summary>
    [JsonPropertyName("ends_at")]
    public DateTimeOffset? EndsAt { get; set; }

    /// <summary>Whether the subscription was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    /// <summary>Date and time the subscription was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the subscription was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// Pause-mode metadata for a subscription.
/// </summary>
public sealed class SubscriptionPause
{
    /// <summary>One of <c>void</c> or <c>free</c>.</summary>
    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    /// <summary>When the pause resumes — null for indefinite pauses.</summary>
    [JsonPropertyName("resumes_at")]
    public DateTimeOffset? ResumesAt { get; set; }
}

/// <summary>
/// Embedded first subscription item — the same shape returned via the
/// <c>/v1/subscription-items/{id}</c> endpoint.
/// </summary>
public sealed class SubscriptionFirstItem
{
    /// <summary>Id of the subscription item (<c>id</c>).</summary>
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    /// <summary>Id of the subscription this item belongs to (<c>subscription_id</c>).</summary>
    [JsonPropertyName("subscription_id")]
    public long? SubscriptionId { get; set; }

    /// <summary>Id of the price associated with the item (<c>price_id</c>).</summary>
    [JsonPropertyName("price_id")]
    public long? PriceId { get; set; }

    /// <summary>Quantity of the subscription item (<c>quantity</c>).</summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>Whether the item is metered/usage-based (<c>is_usage_based</c>).</summary>
    [JsonPropertyName("is_usage_based")]
    public bool? IsUsageBased { get; set; }

    /// <summary>Date and time the item was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the item was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// Self-service URLs exposed on a subscription.
/// </summary>
public sealed class SubscriptionUrls
{
    /// <summary>URL the customer can use to update their payment method (<c>update_payment_method</c>).</summary>
    [JsonPropertyName("update_payment_method")]
    public string? UpdatePaymentMethod { get; set; }

    /// <summary>URL to the customer self-service portal (<c>customer_portal</c>).</summary>
    [JsonPropertyName("customer_portal")]
    public string? CustomerPortal { get; set; }

    /// <summary>
    /// Deep-link into the customer portal that opens the "update subscription" flow
    /// for this specific subscription.
    /// </summary>
    [JsonPropertyName("customer_portal_update_subscription")]
    public string? CustomerPortalUpdateSubscription { get; set; }
}
