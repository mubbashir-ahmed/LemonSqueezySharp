namespace LemonSqueezy.NET.Models.Checkouts;

/// <summary>
/// Inputs for <c>POST /v1/checkouts</c>. Every attribute is optional — the only
/// required fields are <see cref="StoreId"/> and <see cref="VariantId"/> which are
/// sent as JSON:API relationships.
/// </summary>
public sealed class CreateCheckoutRequest
{
    /// <summary>Store the checkout belongs to. Required.</summary>
    public string StoreId { get; set; } = string.Empty;

    /// <summary>Variant the checkout is for. Required.</summary>
    public string VariantId { get; set; } = string.Empty;

    /// <summary>
    /// Override the variant price for this checkout, in cents.
    /// </summary>
    public long? CustomPrice { get; set; }

    /// <summary>Per-checkout product overrides (name, description, post-purchase behaviour).</summary>
    public CheckoutProductOptions? ProductOptions { get; set; }

    /// <summary>Per-checkout UI controls.</summary>
    public CheckoutOptions? CheckoutOptions { get; set; }

    /// <summary>Customer-prefill data (name, email, address, custom fields).</summary>
    public CheckoutData? CheckoutData { get; set; }

    /// <summary>When <c>true</c>, the checkout uses preview mode (no real transaction).</summary>
    public bool? Preview { get; set; }

    /// <summary>Hard expiry for the checkout link.</summary>
    public DateTimeOffset? ExpiresAt { get; set; }

    /// <summary>When <c>true</c>, create the checkout in test mode.</summary>
    public bool? TestMode { get; set; }
}

/// <summary>
/// Wire-format attributes sent inside the JSON:API write envelope.
/// </summary>
internal sealed class CheckoutCreateAttributes
{
    [System.Text.Json.Serialization.JsonPropertyName("custom_price")]
    public long? CustomPrice { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("product_options")]
    public CheckoutProductOptions? ProductOptions { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("checkout_options")]
    public CheckoutOptions? CheckoutOptions { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("checkout_data")]
    public CheckoutData? CheckoutData { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("preview")]
    public bool? Preview { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }
}
