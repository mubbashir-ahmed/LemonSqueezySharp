using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonSqueezySharp.Models.Checkouts;

/// <summary>
/// Checkout attribute payload.
/// See https://docs.lemonsqueezy.com/api/checkouts.
/// </summary>
public sealed class CheckoutAttributes
{
    /// <summary>Identifier of the store the checkout belongs to (<c>store_id</c>).</summary>
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    /// <summary>Identifier of the variant the checkout is for (<c>variant_id</c>).</summary>
    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    /// <summary>
    /// Override the variant price for this checkout, in cents.
    /// </summary>
    [JsonPropertyName("custom_price")]
    public long? CustomPrice { get; set; }

    /// <summary>Per-checkout product overrides (<c>product_options</c>).</summary>
    [JsonPropertyName("product_options")]
    public CheckoutProductOptions? ProductOptions { get; set; }

    /// <summary>Per-checkout UI controls (<c>checkout_options</c>).</summary>
    [JsonPropertyName("checkout_options")]
    public CheckoutOptions? CheckoutOptions { get; set; }

    /// <summary>Customer-prefill data (<c>checkout_data</c>).</summary>
    [JsonPropertyName("checkout_data")]
    public CheckoutData? CheckoutData { get; set; }

    /// <summary>
    /// When <c>true</c>, the checkout uses preview mode (no real transaction).
    /// </summary>
    [JsonPropertyName("preview")]
    public bool? Preview { get; set; }

    /// <summary>Timestamp at which the checkout link expires (<c>expires_at</c>).</summary>
    [JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }

    /// <summary>
    /// The hosted-checkout URL — present on the response of a create call and on get.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>Whether the checkout was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    /// <summary>Timestamp the checkout was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Timestamp the checkout was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}

/// <summary>
/// Per-checkout product overrides (name, description, post-purchase behaviour).
/// </summary>
public sealed class CheckoutProductOptions
{
    /// <summary>Override product name shown on the checkout (<c>name</c>).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>Override product description shown on the checkout (<c>description</c>).</summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>Media identifiers to display on the checkout (<c>media</c>).</summary>
    [JsonPropertyName("media")]
    public IReadOnlyList<string>? Media { get; set; }

    /// <summary>URL the customer is sent to after a successful purchase.</summary>
    [JsonPropertyName("redirect_url")]
    public string? RedirectUrl { get; set; }

    /// <summary>Label for the receipt button (<c>receipt_button_text</c>).</summary>
    [JsonPropertyName("receipt_button_text")]
    public string? ReceiptButtonText { get; set; }

    /// <summary>URL the receipt button links to (<c>receipt_link_url</c>).</summary>
    [JsonPropertyName("receipt_link_url")]
    public string? ReceiptLinkUrl { get; set; }

    /// <summary>Thank-you note shown on the receipt (<c>receipt_thank_you_note</c>).</summary>
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
    /// <summary>Whether to render the checkout in embedded mode (<c>embed</c>).</summary>
    [JsonPropertyName("embed")]
    public bool? Embed { get; set; }

    /// <summary>Whether to show product media on the checkout (<c>media</c>).</summary>
    [JsonPropertyName("media")]
    public bool? Media { get; set; }

    /// <summary>Whether to show the store logo on the checkout (<c>logo</c>).</summary>
    [JsonPropertyName("logo")]
    public bool? Logo { get; set; }

    /// <summary>Whether to show the product description on the checkout (<c>desc</c>).</summary>
    [JsonPropertyName("desc")]
    public bool? Desc { get; set; }

    /// <summary>Whether to show the discount code field on the checkout (<c>discount</c>).</summary>
    [JsonPropertyName("discount")]
    public bool? Discount { get; set; }

    /// <summary>Whether to render the checkout in dark mode (<c>dark</c>).</summary>
    [JsonPropertyName("dark")]
    public bool? Dark { get; set; }

    /// <summary>Whether to show the subscription preview on the checkout (<c>subscription_preview</c>).</summary>
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
    /// <summary>Pre-filled customer email address (<c>email</c>).</summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>Pre-filled customer name (<c>name</c>).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>Pre-filled billing address (<c>billing_address</c>).</summary>
    [JsonPropertyName("billing_address")]
    public CheckoutBillingAddress? BillingAddress { get; set; }

    /// <summary>Pre-filled tax / VAT number (<c>tax_number</c>).</summary>
    [JsonPropertyName("tax_number")]
    public string? TaxNumber { get; set; }

    /// <summary>Pre-applied discount code (<c>discount_code</c>).</summary>
    [JsonPropertyName("discount_code")]
    public string? DiscountCode { get; set; }

    /// <summary>
    /// Arbitrary key/value pairs echoed back on the resulting order's <c>custom_data</c>.
    /// </summary>
    [JsonPropertyName("custom")]
    public JsonElement? Custom { get; set; }

    /// <summary>Per-variant quantity overrides (<c>variant_quantities</c>).</summary>
    [JsonPropertyName("variant_quantities")]
    public IReadOnlyList<CheckoutVariantQuantity>? VariantQuantities { get; set; }
}

/// <summary>
/// Pre-filled billing address for a checkout.
/// </summary>
public sealed class CheckoutBillingAddress
{
    /// <summary>ISO country code (<c>country</c>).</summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    /// <summary>Postal / ZIP code (<c>zip</c>).</summary>
    [JsonPropertyName("zip")]
    public string? Zip { get; set; }
}

/// <summary>
/// Quantity override for a single variant on a checkout.
/// </summary>
public sealed class CheckoutVariantQuantity
{
    /// <summary>Identifier of the variant (<c>variant_id</c>).</summary>
    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    /// <summary>Quantity of the variant (<c>quantity</c>).</summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }
}
