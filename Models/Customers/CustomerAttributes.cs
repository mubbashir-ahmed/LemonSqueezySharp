using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Customers;

/// <summary>
/// Customer attribute payload. Starter set — extend as endpoints land.
/// See https://docs.lemonsqueezy.com/api/customers.
/// </summary>
public sealed class CustomerAttributes
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
