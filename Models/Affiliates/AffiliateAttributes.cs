using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Affiliates;

/// <summary>
/// Affiliate attribute payload.
/// See https://docs.lemonsqueezy.com/api/affiliates.
/// </summary>
public sealed class AffiliateAttributes
{
    /// <summary>Identifier of the store the affiliate belongs to (<c>store_id</c>).</summary>
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    /// <summary>Identifier of the user associated with the affiliate (<c>user_id</c>).</summary>
    [JsonPropertyName("user_id")]
    public long? UserId { get; set; }

    /// <summary>Name of the user associated with the affiliate (<c>user_name</c>).</summary>
    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    /// <summary>Email address of the user associated with the affiliate (<c>user_email</c>).</summary>
    [JsonPropertyName("user_email")]
    public string? UserEmail { get; set; }

    /// <summary>
    /// Domain the affiliate intends to share / promote on.
    /// </summary>
    [JsonPropertyName("share_domain")]
    public string? ShareDomain { get; set; }

    /// <summary>One of <c>active</c>, <c>pending</c>, <c>rejected</c>, <c>disabled</c>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Subset of product ids the affiliate is restricted to — <see langword="null"/>
    /// when the affiliate may promote any product on the store.
    /// </summary>
    [JsonPropertyName("products")]
    public IReadOnlyList<long>? Products { get; set; }

    /// <summary>Free-text note the affiliate submitted with their application.</summary>
    [JsonPropertyName("application_note")]
    public string? ApplicationNote { get; set; }

    /// <summary>All-time gross commission earned by the affiliate, in cents.</summary>
    [JsonPropertyName("total_earnings")]
    public long? TotalEarnings { get; set; }

    /// <summary>Commission earned but not yet paid out to the affiliate, in cents.</summary>
    [JsonPropertyName("unpaid_earnings")]
    public long? UnpaidEarnings { get; set; }

    /// <summary>Date and time the affiliate was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the affiliate was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
