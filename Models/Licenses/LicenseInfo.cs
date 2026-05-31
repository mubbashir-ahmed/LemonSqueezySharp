using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Licenses;

/// <summary>
/// Flat summary of a license key, embedded in License-API responses (different shape
/// from the JSON:API <c>LicenseKey</c> resource).
/// </summary>
public sealed class LicenseInfo
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("activation_limit")]
    public int? ActivationLimit { get; set; }

    [JsonPropertyName("activation_usage")]
    public int? ActivationUsage { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }

    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }
}

/// <summary>
/// Flat summary of a license-key instance, embedded in License-API responses.
/// </summary>
public sealed class LicenseInstanceInfo
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }
}

/// <summary>
/// Order / customer context attached to License-API responses.
/// </summary>
public sealed class LicenseMeta
{
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    [JsonPropertyName("order_item_id")]
    public long? OrderItemId { get; set; }

    [JsonPropertyName("product_id")]
    public long? ProductId { get; set; }

    [JsonPropertyName("product_name")]
    public string? ProductName { get; set; }

    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    [JsonPropertyName("variant_name")]
    public string? VariantName { get; set; }

    [JsonPropertyName("customer_id")]
    public long? CustomerId { get; set; }

    [JsonPropertyName("customer_name")]
    public string? CustomerName { get; set; }

    [JsonPropertyName("customer_email")]
    public string? CustomerEmail { get; set; }
}
