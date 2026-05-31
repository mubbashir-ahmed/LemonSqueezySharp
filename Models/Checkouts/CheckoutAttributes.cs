using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Checkouts;

/// <summary>
/// Checkout attribute payload.
/// See https://docs.lemonsqueezy.com/api/checkouts.
/// </summary>
public sealed class CheckoutAttributes
{
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    /// <summary>
    /// Override the variant price for this checkout, in cents.
    /// </summary>
    [JsonPropertyName("custom_price")]
    public long? CustomPrice { get; set; }

    [JsonPropertyName("product_options")]
    public CheckoutProductOptions? ProductOptions { get; set; }

    [JsonPropertyName("checkout_options")]
    public CheckoutOptions? CheckoutOptions { get; set; }

    [JsonPropertyName("checkout_data")]
    public CheckoutData? CheckoutData { get; set; }

    /// <summary>
    /// When <c>true</c>, the checkout uses preview mode (no real transaction).
    /// </summary>
    [JsonPropertyName("preview")]
    public bool? Preview { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }

    /// <summary>
    /// The hosted-checkout URL — present on the response of a create call and on get.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// Per-checkout product overrides (name, description, post-purchase behaviour).
/// </summary>
public sealed class CheckoutProductOptions
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("media")]
    public IReadOnlyList<string>? Media { get; set; }

    /// <summary>URL the customer is sent to after a successful purchase.</summary>
    [JsonPropertyName("redirect_url")]
    public string? RedirectUrl { get; set; }

    [JsonPropertyName("receipt_button_text")]
    public string? ReceiptButtonText { get; set; }

    [JsonPropertyName("receipt_link_url")]
    public string? ReceiptLinkUrl { get; set; }

    [JsonPropertyName("receipt_thank_you_note")]
    public string? ReceiptThankYouNote { get; set; }

    /// <summary>
    /// Restrict the checkout to a subset of the product's variants.
    /// </summary>
    [JsonPropertyName("enabled_variants")]
    public IReadOnlyList<long>? EnabledVariants { get; set; }
}

/// <summary>
/// Per-checkout UI controls.
/// </summary>
public sealed class CheckoutOptions
{
    [JsonPropertyName("embed")]
    public bool? Embed { get; set; }

    [JsonPropertyName("media")]
    public bool? Media { get; set; }

    [JsonPropertyName("logo")]
    public bool? Logo { get; set; }

    [JsonPropertyName("desc")]
    public bool? Desc { get; set; }

    [JsonPropertyName("discount")]
    public bool? Discount { get; set; }

    [JsonPropertyName("dark")]
    public bool? Dark { get; set; }

    [JsonPropertyName("subscription_preview")]
    public bool? SubscriptionPreview { get; set; }

    /// <summary>Hex colour for the checkout button (e.g. <c>"#7047EB"</c>).</summary>
    [JsonPropertyName("button_color")]
    public string? ButtonColor { get; set; }
}

/// <summary>
/// Customer-prefill data — name, email, address, custom fields.
/// </summary>
public sealed class CheckoutData
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("billing_address")]
    public CheckoutBillingAddress? BillingAddress { get; set; }

    [JsonPropertyName("tax_number")]
    public string? TaxNumber { get; set; }

    [JsonPropertyName("discount_code")]
    public string? DiscountCode { get; set; }

    /// <summary>
    /// Arbitrary key/value pairs echoed back on the resulting order's <c>custom_data</c>.
    /// </summary>
    [JsonPropertyName("custom")]
    public JsonElement? Custom { get; set; }

    [JsonPropertyName("variant_quantities")]
    public IReadOnlyList<CheckoutVariantQuantity>? VariantQuantities { get; set; }
}

public sealed class CheckoutBillingAddress
{
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("zip")]
    public string? Zip { get; set; }
}

public sealed class CheckoutVariantQuantity
{
    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }
}
