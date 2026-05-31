using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.LicenseKeys;

/// <summary>
/// License-key attribute payload.
/// See https://docs.lemonsqueezy.com/api/license-keys.
/// </summary>
public sealed class LicenseKeyAttributes
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

    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    [JsonPropertyName("user_email")]
    public string? UserEmail { get; set; }

    /// <summary>The license key itself.</summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    /// <summary>The last four characters of the key — safe to show in UIs.</summary>
    [JsonPropertyName("key_short")]
    public string? KeyShort { get; set; }

    /// <summary>
    /// Maximum number of activations allowed. <see langword="null"/> for unlimited.
    /// </summary>
    [JsonPropertyName("activation_limit")]
    public int? ActivationLimit { get; set; }

    [JsonPropertyName("instances_count")]
    public int? InstancesCount { get; set; }

    [JsonPropertyName("disabled")]
    public bool? Disabled { get; set; }

    /// <summary>One of <c>inactive</c>, <c>active</c>, <c>expired</c>, <c>disabled</c>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
