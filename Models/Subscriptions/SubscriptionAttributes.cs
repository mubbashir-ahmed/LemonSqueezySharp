using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Subscriptions;

/// <summary>
/// Subscription attribute payload.
/// See https://docs.lemonsqueezy.com/api/subscriptions.
/// </summary>
public sealed class SubscriptionAttributes
{
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    [JsonPropertyName("customer_id")]
    public long? CustomerId { get; set; }

    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    [JsonPropertyName("order_item_id")]
    public long? OrderItemId { get; set; }

    [JsonPropertyName("product_id")]
    public long? ProductId { get; set; }

    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    [JsonPropertyName("product_name")]
    public string? ProductName { get; set; }

    [JsonPropertyName("variant_name")]
    public string? VariantName { get; set; }

    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    [JsonPropertyName("user_email")]
    public string? UserEmail { get; set; }

    [JsonPropertyName("status")]
    public SubscriptionStatus? Status { get; set; }

    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    [JsonPropertyName("card_brand")]
    public string? CardBrand { get; set; }

    [JsonPropertyName("card_last_four")]
    public string? CardLastFour { get; set; }

    [JsonPropertyName("pause")]
    public SubscriptionPause? Pause { get; set; }

    [JsonPropertyName("cancelled")]
    public bool? Cancelled { get; set; }

    [JsonPropertyName("trial_ends_at")]
    public DateTimeOffset? TrialEndsAt { get; set; }

    [JsonPropertyName("billing_anchor")]
    public int? BillingAnchor { get; set; }

    [JsonPropertyName("first_subscription_item")]
    public SubscriptionFirstItem? FirstSubscriptionItem { get; set; }

    [JsonPropertyName("urls")]
    public SubscriptionUrls? Urls { get; set; }

    [JsonPropertyName("renews_at")]
    public DateTimeOffset? RenewsAt { get; set; }

    [JsonPropertyName("ends_at")]
    public DateTimeOffset? EndsAt { get; set; }

    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

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
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("subscription_id")]
    public long? SubscriptionId { get; set; }

    [JsonPropertyName("price_id")]
    public long? PriceId { get; set; }

    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    [JsonPropertyName("is_usage_based")]
    public bool? IsUsageBased { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// Self-service URLs exposed on a subscription.
/// </summary>
public sealed class SubscriptionUrls
{
    [JsonPropertyName("update_payment_method")]
    public string? UpdatePaymentMethod { get; set; }

    [JsonPropertyName("customer_portal")]
    public string? CustomerPortal { get; set; }

    /// <summary>
    /// Deep-link into the customer portal that opens the "update subscription" flow
    /// for this specific subscription.
    /// </summary>
    [JsonPropertyName("customer_portal_update_subscription")]
    public string? CustomerPortalUpdateSubscription { get; set; }
}
