using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Products;

/// <summary>
/// Product attribute payload.
/// See https://docs.lemonsqueezy.com/api/products.
/// </summary>
public sealed class ProductAttributes
{
    [JsonPropertyName("store_id")]
    public long? StoreId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("status_formatted")]
    public string? StatusFormatted { get; set; }

    [JsonPropertyName("thumb_url")]
    public string? ThumbUrl { get; set; }

    [JsonPropertyName("large_thumb_url")]
    public string? LargeThumbUrl { get; set; }

    [JsonPropertyName("price")]
    public long? Price { get; set; }

    [JsonPropertyName("price_formatted")]
    public string? PriceFormatted { get; set; }

    [JsonPropertyName("from_price")]
    public long? FromPrice { get; set; }

    [JsonPropertyName("to_price")]
    public long? ToPrice { get; set; }

    [JsonPropertyName("pay_what_you_want")]
    public bool? PayWhatYouWant { get; set; }

    [JsonPropertyName("buy_now_url")]
    public string? BuyNowUrl { get; set; }

    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
