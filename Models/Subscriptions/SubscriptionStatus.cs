using System.Runtime.Serialization;

namespace LemonSqueezy.NET.Models.Subscriptions;

/// <summary>
/// Lifecycle status of a subscription.
/// Matches the snake_case strings returned by the API; deserialized via
/// the snake-case enum converter registered in <c>LemonSqueezyJsonOptions</c>.
/// </summary>
public enum SubscriptionStatus
{
    [EnumMember(Value = "on_trial")]
    OnTrial,
    [EnumMember(Value = "active")]
    Active,
    [EnumMember(Value = "paused")]
    Paused,
    [EnumMember(Value = "past_due")]
    PastDue,
    [EnumMember(Value = "unpaid")]
    Unpaid,
    [EnumMember(Value = "cancelled")]
    Cancelled,
    [EnumMember(Value = "expired")]
    Expired,
}
