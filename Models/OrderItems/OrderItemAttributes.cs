using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.OrderItems;

/// <summary>
/// Order-item attribute payload.
/// See https://docs.lemonsqueezy.com/api/order-items.
/// </summary>
public sealed class OrderItemAttributes
{
    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    [JsonPropertyName("product_id")]
    public long? ProductId { get; set; }

    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    [JsonPropertyName("product_name")]
    public string? ProductName { get; set; }

    [JsonPropertyName("variant_name")]
    public string? VariantName { get; set; }

    [JsonPropertyName("price")]
    public long? Price { get; set; }

    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
