using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Customers;

/// <summary>
/// Customer attribute payload.
/// See https://docs.lemonsqueezy.com/api/customers.
/// </summary>
public sealed class CustomerAttributes
{
    /// <summary>Identifier of the store the customer belongs to (<c>store_id</c>).</summary>
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    /// <summary>Customer full name (<c>name</c>).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>Customer email address (<c>email</c>).</summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>Current customer status (<c>status</c>).</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>Human-readable status label (<c>status_formatted</c>).</summary>
    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    /// <summary>Customer city (<c>city</c>).</summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }

    /// <summary>Customer state or region (<c>region</c>).</summary>
    [JsonPropertyName("region")]
    public string? Region { get; set; }

    /// <summary>Customer ISO country code (<c>country</c>).</summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    /// <summary>Human-readable country name (<c>country_formatted</c>).</summary>
    [JsonPropertyName("country_formatted")]
    public string? CountryFormatted { get; set; }

    /// <summary>Total revenue from the customer, in cents (<c>total_revenue_currency</c>).</summary>
    [JsonPropertyName("total_revenue_currency")]
    public long? TotalRevenueCurrency { get; set; }

    /// <summary>Human-readable total revenue (<c>total_revenue_currency_formatted</c>).</summary>
    [JsonPropertyName("total_revenue_currency_formatted")]
    public string? TotalRevenueCurrencyFormatted { get; set; }

    /// <summary>Monthly recurring revenue from the customer, in cents (<c>mrr</c>).</summary>
    [JsonPropertyName("mrr")]
    public long? Mrr { get; set; }

    /// <summary>Human-readable monthly recurring revenue (<c>mrr_formatted</c>).</summary>
    [JsonPropertyName("mrr_formatted")]
    public string? MrrFormatted { get; set; }

    /// <summary>Self-service links exposed for the customer (<c>urls</c>).</summary>
    [JsonPropertyName("urls")]
    public CustomerUrls? Urls { get; set; }

    /// <summary>Whether the customer was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    /// <summary>Timestamp the customer was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Timestamp the customer was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// Self-service links exposed for a customer (e.g. customer portal).
/// </summary>
public sealed class CustomerUrls
{
    /// <summary>Signed URL to the customer portal (<c>customer_portal</c>).</summary>
    [JsonPropertyName("customer_portal")]
    public string? CustomerPortal { get; set; }
}
