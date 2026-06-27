using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Models.OrderItems;

/// <summary>
/// Order-item attribute payload.
/// See https://docs.lemonsqueezy.com/api/order-items.
/// </summary>
public sealed class OrderItemAttributes
{
    /// <summary>Id of the order this item belongs to (<c>order_id</c>).</summary>
    [JsonPropertyName("order_id")]
    public long? OrderId { get; set; }

    /// <summary>Id of the purchased product (<c>product_id</c>).</summary>
    [JsonPropertyName("product_id")]
    public long? ProductId { get; set; }

    /// <summary>Id of the purchased variant (<c>variant_id</c>).</summary>
    [JsonPropertyName("variant_id")]
    public long? VariantId { get; set; }

    /// <summary>Id of the price associated with the item (<c>price_id</c>).</summary>
    [JsonPropertyName("price_id")]
    public long? PriceId { get; set; }

    /// <summary>Name of the purchased product (<c>product_name</c>).</summary>
    [JsonPropertyName("product_name")]
    public string? ProductName { get; set; }

    /// <summary>Name of the purchased variant (<c>variant_name</c>).</summary>
    [JsonPropertyName("variant_name")]
    public string? VariantName { get; set; }

    /// <summary>Price of the item in cents (<c>price</c>).</summary>
    [JsonPropertyName("price")]
    public long? Price { get; set; }

    /// <summary>Quantity purchased (<c>quantity</c>).</summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>Whether the item was created in test mode (<c>test_mode</c>).</summary>
    [JsonPropertyName("test_mode")]
    public bool? TestMode { get; set; }

    /// <summary>Date and time the item was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Date and time the item was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
