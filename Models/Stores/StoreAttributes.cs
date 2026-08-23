using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.Stores;

/// <summary>
/// Attribute payload for a LemonSqueezy store.
/// See https://docs.lemonsqueezy.com/api/stores.
/// </summary>
public sealed class StoreAttributes
{
    /// <summary>Store name (<c>name</c>).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>URL-friendly store slug (<c>slug</c>).</summary>
    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    /// <summary>Store domain (<c>domain</c>).</summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>Public store URL (<c>url</c>).</summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>URL of the store avatar (<c>avatar_url</c>).</summary>
    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }

    /// <summary>Subscription plan the store is on (<c>plan</c>).</summary>
    [JsonPropertyName("plan")]
    public string? Plan { get; set; }

    /// <summary>Store ISO country code (<c>country</c>).</summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    /// <summary>Human-readable country name (<c>country_nicename</c>).</summary>
    [JsonPropertyName("country_nicename")]
    public string? CountryNicename { get; set; }

    /// <summary>Store default currency code (<c>currency</c>).</summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>All-time number of sales (<c>total_sales</c>).</summary>
    [JsonPropertyName("total_sales")]
    public long? TotalSales { get; set; }

    /// <summary>All-time revenue, in cents (<c>total_revenue</c>).</summary>
    [JsonPropertyName("total_revenue")]
    public long? TotalRevenue { get; set; }

    /// <summary>Number of sales in the last 30 days (<c>thirty_day_sales</c>).</summary>
    [JsonPropertyName("thirty_day_sales")]
    public long? ThirtyDaySales { get; set; }

    /// <summary>Revenue in the last 30 days, in cents (<c>thirty_day_revenue</c>).</summary>
    [JsonPropertyName("thirty_day_revenue")]
    public long? ThirtyDayRevenue { get; set; }

    /// <summary>Timestamp the store was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Timestamp the store was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
