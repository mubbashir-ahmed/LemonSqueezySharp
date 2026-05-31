using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.Products;

/// <summary>
/// Product attribute payload. Starter set — extend as endpoints land.
/// See https://docs.lemonsqueezy.com/api/products.
/// </summary>
public sealed class ProductAttributes
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("price")]
    public long? Price { get; set; }

    [JsonPropertyName("buy_now_url")]
    public string? BuyNowUrl { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
