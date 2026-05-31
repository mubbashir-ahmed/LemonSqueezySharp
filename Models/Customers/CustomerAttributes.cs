using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Customers;

/// <summary>
/// Customer attribute payload.
/// See https://docs.lemonsqueezy.com/api/customers.
/// </summary>
public sealed class CustomerAttributes
{
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("country_formatted")]
    public string? CountryFormatted { get; set; }

    [JsonPropertyName("total_revenue_currency")]
    public long? TotalRevenueCurrency { get; set; }

    [JsonPropertyName("total_revenue_currency_formatted")]
    public string? TotalRevenueCurrencyFormatted { get; set; }

    [JsonPropertyName("mrr")]
    public long? Mrr { get; set; }

    [JsonPropertyName("mrr_formatted")]
    public string? MrrFormatted { get; set; }

    [JsonPropertyName("urls")]
    public CustomerUrls? Urls { get; set; }

    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// Self-service links exposed for a customer (e.g. customer portal).
/// </summary>
public sealed class CustomerUrls
{
    [JsonPropertyName("customer_portal")]
    public string? CustomerPortal { get; set; }
}
