using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.LicenseKeys;

/// <summary>
/// License-key attribute payload.
/// See https://docs.lemonsqueezy.com/api/license-keys.
/// </summary>
public sealed class LicenseKeyAttributes
{
    /// <summary>Identifier of the store the license key belongs to (<c>store_id</c>).</summary>
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    /// <summary>Identifier of the customer the license key belongs to (<c>customer_id</c>).</summary>
    [JsonPropertyName("customer_id")]
    public long? CustomerId { get; set; }

    /// <summary>Identifier of the order that created the license key (<c>order_id</c>).</summary>
    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    /// <summary>Identifier of the order item that created the license key (<c>order_item_id</c>).</summary>
    [JsonPropertyName("order_item_id")]
    public long? OrderItemId { get; set; }

    /// <summary>Identifier of the product associated with the license key (<c>product_id</c>).</summary>
    [JsonPropertyName("product_id")]
    public long? ProductId { get; set; }

    /// <summary>Name of the customer the license key was issued to (<c>user_name</c>).</summary>
    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    /// <summary>Email address of the customer the license key was issued to (<c>user_email</c>).</summary>
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

    /// <summary>Number of activation instances currently registered against the key (<c>instances_count</c>).</summary>
    [JsonPropertyName("instances_count")]
    public int? InstancesCount { get; set; }

    /// <summary>Whether the license key has been disabled (<c>disabled</c>).</summary>
    [JsonPropertyName("disabled")]
    public bool? Disabled { get; set; }

    /// <summary>One of <c>inactive</c>, <c>active</c>, <c>expired</c>, <c>disabled</c>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>Human-readable form of <see cref="Status"/> (<c>status_formatted</c>).</summary>
    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    /// <summary>Date and time the license key expires (<c>expires_at</c>).</summary>
    [JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }

    /// <summary>Date and time the license key was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the license key was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
