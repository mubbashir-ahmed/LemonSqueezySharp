using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Products;

/// <summary>
/// Product attribute payload.
/// See https://docs.lemonsqueezy.com/api/products.
/// </summary>
public sealed class ProductAttributes
{
    /// <summary>Identifier of the store the product belongs to (<c>store_id</c>).</summary>
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    /// <summary>Product name (<c>name</c>).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>URL-friendly product slug (<c>slug</c>).</summary>
    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    /// <summary>Product description, may contain HTML (<c>description</c>).</summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>Current product status (<c>status</c>).</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>Human-readable status label (<c>status_formatted</c>).</summary>
    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    /// <summary>URL of the product thumbnail (<c>thumb_url</c>).</summary>
    [JsonPropertyName("thumb_url")]
    public string? ThumbUrl { get; set; }

    /// <summary>URL of the large product thumbnail (<c>large_thumb_url</c>).</summary>
    [JsonPropertyName("large_thumb_url")]
    public string? LargeThumbUrl { get; set; }

    /// <summary>Product price, in cents (<c>price</c>).</summary>
    [JsonPropertyName("price")]
    public long? Price { get; set; }

    /// <summary>Human-readable product price (<c>price_formatted</c>).</summary>
    [JsonPropertyName("price_formatted")]
    public string? PriceFormatted { get; set; }

    /// <summary>Lowest variant price, in cents (<c>from_price</c>).</summary>
    [JsonPropertyName("from_price")]
    public long? FromPrice { get; set; }

    /// <summary>Highest variant price, in cents (<c>to_price</c>).</summary>
    [JsonPropertyName("to_price")]
    public long? ToPrice { get; set; }

    /// <summary>Whether the product uses pay-what-you-want pricing (<c>pay_what_you_want</c>).</summary>
    [JsonPropertyName("pay_what_you_want")]
    public bool? PayWhatYouWant { get; set; }

    /// <summary>Direct buy-now checkout URL for the product (<c>buy_now_url</c>).</summary>
    [JsonPropertyName("buy_now_url")]
    public string? BuyNowUrl { get; set; }

    /// <summary>Whether the product was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    /// <summary>Timestamp the product was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Timestamp the product was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
