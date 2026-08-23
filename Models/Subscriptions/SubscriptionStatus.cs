using System.Runtime.Serialization;

namespace LemonSqueezySharp.Models.Subscriptions;

/// <summary>
/// Lifecycle status of a subscription.
/// Matches the snake_case strings returned by the API; deserialized via
/// the snake-case enum converter registered in <c>LemonSqueezyJsonOptions</c>.
/// </summary>
public enum SubscriptionStatus
{
    /// <summary>Subscription is in its trial period (<c>on_trial</c>).</summary>
    [EnumMember(Value = "on_trial")]
    OnTrial,

    /// <summary>Subscription is active and paid (<c>active</c>).</summary>
    [EnumMember(Value = "active")]
    Active,

    /// <summary>Subscription is paused (<c>paused</c>).</summary>
    [EnumMember(Value = "paused")]
    Paused,

    /// <summary>A payment has failed and the subscription is past due (<c>past_due</c>).</summary>
    [EnumMember(Value = "past_due")]
    PastDue,

    /// <summary>Payment retries have been exhausted and the subscription is unpaid (<c>unpaid</c>).</summary>
    [EnumMember(Value = "unpaid")]
    Unpaid,

    /// <summary>Subscription has been cancelled (<c>cancelled</c>).</summary>
    [EnumMember(Value = "cancelled")]
    Cancelled,

    /// <summary>Subscription has expired (<c>expired</c>).</summary>
    [EnumMember(Value = "expired")]
    Expired,
}
