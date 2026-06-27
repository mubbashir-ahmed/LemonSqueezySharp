using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Licenses;

/// <summary>
/// Flat summary of a license key, embedded in License-API responses (different shape
/// from the JSON:API <c>LicenseKey</c> resource).
/// </summary>
public sealed class LicenseInfo
{
    /// <summary>Identifier of the license key (<c>id</c>).</summary>
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    /// <summary>Current status of the license key (<c>status</c>).</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>The license key itself (<c>key</c>).</summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    /// <summary>Maximum number of activations allowed (<c>activation_limit</c>).</summary>
    [JsonPropertyName("activation_limit")]
    public int? ActivationLimit { get; set; }

    /// <summary>Number of activations currently in use (<c>activation_usage</c>).</summary>
    [JsonPropertyName("activation_usage")]
    public int? ActivationUsage { get; set; }

    /// <summary>Date and time the license key was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the license key expires (<c>expires_at</c>).</summary>
    [JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }

    /// <summary>Whether the license key was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }
}

/// <summary>
/// Flat summary of a license-key instance, embedded in License-API responses.
/// </summary>
public sealed class LicenseInstanceInfo
{
    /// <summary>Unique identifier of the activation instance (<c>id</c>).</summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>Caller-supplied label for the activation instance (<c>name</c>).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>Date and time the activation instance was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }
}

/// <summary>
/// Order / customer context attached to License-API responses.
/// </summary>
public sealed class LicenseMeta
{
    /// <summary>Identifier of the store the license key belongs to (<c>store_id</c>).</summary>
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    /// <summary>Identifier of the order that created the license key (<c>order_id</c>).</summary>
    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    /// <summary>Identifier of the order item that created the license key (<c>order_item_id</c>).</summary>
    [JsonPropertyName("order_item_id")]
    public long? OrderItemId { get; set; }

    /// <summary>Identifier of the product associated with the license key (<c>product_id</c>).</summary>
    [JsonPropertyName("product_id")]
    public long? ProductId { get; set; }

    /// <summary>Name of the product associated with the license key (<c>product_name</c>).</summary>
    [JsonPropertyName("product_name")]
    public string? ProductName { get; set; }

    /// <summary>Identifier of the variant associated with the license key (<c>variant_id</c>).</summary>
    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    /// <summary>Name of the variant associated with the license key (<c>variant_name</c>).</summary>
    [JsonPropertyName("variant_name")]
    public string? VariantName { get; set; }

    /// <summary>Identifier of the customer the license key was issued to (<c>customer_id</c>).</summary>
    [JsonPropertyName("customer_id")]
    public long? CustomerId { get; set; }

    /// <summary>Name of the customer the license key was issued to (<c>customer_name</c>).</summary>
    [JsonPropertyName("customer_name")]
    public string? CustomerName { get; set; }

    /// <summary>Email address of the customer the license key was issued to (<c>customer_email</c>).</summary>
    [JsonPropertyName("customer_email")]
    public string? CustomerEmail { get; set; }
}
